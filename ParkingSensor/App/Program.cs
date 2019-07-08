using System;
using System.Reflection;
using App.Core;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using App.Fakes;
using App.Infrastructure.Sensors;
using MediatR;
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
            var collection = new ServiceCollection();
            collection.AddLogging(configure => configure.AddConsole());

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEvent));
            collection.AddMediatR(coreAssembly);

            collection.AddSingleton<IBuzzer>(x =>
            {
                return new FakeBuzzer();
//#if DEBUG
//                return new FakeBuzzer();
//#else
//                return new PiezoBuzzerController(17);
//#endif
            });

            collection.AddSingleton<IMeasureSensor>(x =>
            {
#if DEBUG
                return new FakeMeasureSensor();
#else
                return new Hcsr04Sensor(23, 24);
#endif
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