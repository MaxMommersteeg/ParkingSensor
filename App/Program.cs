using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using App.Core;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Events;
using App.Core.PipelineBehaviors;
using App.Infrastructure.Devices;
using App.Infrastructure.Sensors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App
{
    public static class Program
    {
        private static IServiceProvider _serviceProvider;

        public static Task Main(string[] args)
        {
            return CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();

                    var configuration = builder.Build();

                    var collection = new ServiceCollection();
                    services.AddLogging(configure =>
                    {
                        configure.AddConsole();
                        configure.AddConfiguration(configuration.GetSection("Logging"));
                    });

                    var coreAssembly = Assembly.GetAssembly(typeof(BaseEvent));
                    services.AddMediatR(coreAssembly);
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

                    var sensorsConfig = configuration.GetSection("Sensors");

                    services.AddSingleton<IBuzzer>(x =>
                    {
                        var piezoBuzzerConfig = sensorsConfig.GetSection("PiezoBuzzer");
                        if (piezoBuzzerConfig.GetValue("UseSimulated", defaultValue: false))
                        {
                            return new SimulatedBuzzer();
                        }

                        return new PiezoBuzzerController(piezoBuzzerConfig.GetValue<int>("PinNumber"));
                    });

                    services.AddSingleton<IMeasureSensor>(x =>
                    {
                        var hcSr04Config = sensorsConfig.GetSection("HcSr04");
                        if (hcSr04Config.GetValue("UseSimulated", defaultValue: false))
                        {
                            return new SimulatedMeasureSensor();
                        }

                        return new Hcsr04Sensor(hcSr04Config.GetValue<int>("TriggerPin"), hcSr04Config.GetValue<int>("EchoPin"));
                    });

                    services.AddSingleton<IMotionDetectionSensor>(x =>
                    {
                        var hcSr501Config = sensorsConfig.GetSection("HcSr501");
                        if (hcSr501Config.GetValue("UseDummy", defaultValue: false))
                        {
                            return new DummyMotionDetectionSensor();
                        }

                        var mediator = _serviceProvider.GetService<IMediator>();
                        return new Hcsr501Sensor(mediator, hcSr501Config.GetValue<int>("OutPin"));
                    });

                    services.AddSingleton<IDistanceToSoundEffectConverter, DistanceToSoundEffectConverter>();
                    services.AddScoped<IDistanceMeasurementService, DistanceMeasurementService>();
                    services.AddScoped<IMotionDetectionService, MotionDetectionService>();
                    services.AddScoped<IBuzzerService, BuzzerService>();
                    services.AddScoped<IParkingSensorService, ParkingSensorService>();

                    _serviceProvider = services.BuildServiceProvider();
                });
        }
    }
}