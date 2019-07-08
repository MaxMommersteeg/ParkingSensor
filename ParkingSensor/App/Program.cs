using System;
using System.IO;
using System.Reflection;
using App.Core;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using App.Infrastructure.Devices;
using App.Infrastructure.Sensors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App
{
    public class Program
    {
        private static IServiceProvider _serviceProvider;

        public static void Main()
        {
            Console.WriteLine("Started Parking Sensor app.");

            // Setup dependency injection.
            RegisterServices();

            var messagingMediator = _serviceProvider.GetService<IMediator>();
            messagingMediator.Send(new StartDistanceMeasurement(TimeSpan.FromSeconds(5)));

            Console.ReadKey();

            // Cleanup.
            DisposeServices();
        }

        public static void RegisterServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var collection = new ServiceCollection();
            collection.AddLogging(configure => configure.AddConsole());

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEvent));
            collection.AddMediatR(coreAssembly);

            var sensorsConfig = configuration.GetSection("sensors");

            collection.AddSingleton<IBuzzer>(x =>
            {
                var piezoBuzzerConfig = sensorsConfig.GetSection("piezoBuzzer");
                if (piezoBuzzerConfig.GetValue("useSimluated", defaultValue: false))
                {
                    return new SimulatedBuzzer();
                }

                return new PiezoBuzzerController(piezoBuzzerConfig.GetValue<int>("pinNumber"));
            });

            collection.AddSingleton<IMeasureSensor>(x =>
            {
                var hcSr04Config = sensorsConfig.GetSection("hcSr04");
                if (hcSr04Config.GetValue("useSimluated", defaultValue: false))
                {
                    return new SimulatedMeasureSensor();
                }

                return new Hcsr04Sensor(hcSr04Config.GetValue<int>("triggerPin"), hcSr04Config.GetValue<int>("echoPin"));
            });

            collection.AddSingleton<IDistanceToToneFrequencyConverter, DistanceToToneFrequencyConverter>();
            collection.AddScoped<IDistanceMeasurementService, DistanceMeasurementService>();
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

            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
;