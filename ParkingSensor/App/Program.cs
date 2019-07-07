using System;
using System.Reflection;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Messages.Commands;
using App.Core.Messages.Events;
using App.Infrastructure.Sensors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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
            messagingMediator.Send(new StartDistanceMeasuringService(23, 24));
            messagingMediator.Send(new StartParkingService());

            using (var distanceSensor = _serviceProvider.GetService<IMeasureSensor>())
            using (var buzzer = _serviceProvider.GetService<IBuzzer>())
            using (var parkingSensorService = new ParkingSensorService(distanceToToneFrequencyConverter))
            {
                Console.WriteLine("All services initialized.");

                distanceSensor.Start();

                Console.WriteLine("Started distance sensor.");

                parkingSensorService.Start();

                Console.WriteLine("Started parking sensor service.");

                Console.ReadKey();
            }

            // Cleanup.
            DisposeServices();
        }

        public static void RegisterServices()
        {
            var collection = new ServiceCollection();

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEvent));
            collection.AddMediatR(coreAssembly);

            collection.AddSingleton<IBuzzer>(x =>
            {
                return new PiezoBuzzerController(17);
            });

            collection.AddSingleton<IMeasureSensor>(x =>
            {
                var messagingMediator = _serviceProvider.GetService<IMediator>();
                return new Hcsr04Sensor(messagingMediator, 23, 24);
            });

            collection.AddSingleton<IParkingSensorService, ParkingSensorService>();

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