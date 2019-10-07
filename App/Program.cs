using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using App.Core;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using App.Core.PipelineBehaviors;
using App.Infrastructure.Devices;
using App.Infrastructure.Sensors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App
{
    public static class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IConfiguration _configuration;

        public static async Task Main()
        {
            Console.WriteLine("Started Parking Sensor app.");

            // Setup dependency injection.
            RegisterServices();

            var distanceMeasuringConfig = _configuration.GetSection("DistanceMeasuring");

            var mediator = _serviceProvider.GetService<IMediator>();
            await mediator.Send(new StartDistanceMeasurement(TimeSpan.FromSeconds(distanceMeasuringConfig.GetValue<int>("IntervalInSeconds"))));
            await mediator.Send(new StartMotionDetection());

            Console.ReadKey();

            // Cleanup.
            DisposeServices();
        }

        private static void RegisterServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var collection = new ServiceCollection();
            collection.AddLogging(configure =>
            {
                configure.AddConsole();
                configure.AddConfiguration(_configuration.GetSection("Logging"));
            });

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEvent));
            collection.AddMediatR(coreAssembly);
            collection.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            var sensorsConfig = _configuration.GetSection("Sensors");

            collection.AddSingleton<IBuzzer>(x =>
            {
                var piezoBuzzerConfig = sensorsConfig.GetSection("PiezoBuzzer");
                if (piezoBuzzerConfig.GetValue("UseSimulated", defaultValue: false))
                {
                    return new SimulatedBuzzer();
                }

                return new PiezoBuzzerController(piezoBuzzerConfig.GetValue<int>("PinNumber"));
            });

            collection.AddSingleton<IMeasureSensor>(x =>
            {
                var hcSr04Config = sensorsConfig.GetSection("HcSr04");
                if (hcSr04Config.GetValue("UseSimulated", defaultValue: false))
                {
                    return new SimulatedMeasureSensor();
                }

                return new Hcsr04Sensor(hcSr04Config.GetValue<int>("TriggerPin"), hcSr04Config.GetValue<int>("EchoPin"));
            });

            collection.AddSingleton<IMotionDetectionSensor>(x =>
            {
                var hcSr501Config = sensorsConfig.GetSection("HcSr501");
                if (hcSr501Config.GetValue("UseDummy", defaultValue: false))
                {
                    return new DummyMotionDetectionSensor();
                }

                var mediator = _serviceProvider.GetService<IMediator>();
                return new Hcsr501Sensor(mediator, hcSr501Config.GetValue<int>("OutPin"));
            });

            collection.AddSingleton<IDistanceToSoundEffectConverter, DistanceToSoundEffectConverter>();
            collection.AddScoped<IDistanceMeasurementService, DistanceMeasurementService>();
            collection.AddScoped<IMotionDetectionService, MotionDetectionService>();
            collection.AddScoped<IBuzzerService, BuzzerService>();
            collection.AddScoped<IParkingSensorService, ParkingSensorService>();

            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }

            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}