using System;
using System.Linq;
using System.Reflection;
using App.Core;
using App.Core.Devices;
using App.Core.DomainServices;
using App.Core.Entities;
using App.Core.Interfaces;
using App.Core.SharedKernel;
using App.Infrastructure.DomainEvents;
using App.Infrastructure.Sensors;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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

            var distanceToToneFrequencyConverter = _serviceProvider.GetService<IDistanceToToneFrequencyConverter>();
            var distanceMeasurement = new DistanceMeasurement(15);
            distanceMeasurement.MarkMeasured();

            var dispatcher = _serviceProvider.GetService<IDomainEventDispatcher>();
            dispatcher.Dispatch(distanceMeasurement.Events.First());

            Console.ReadKey();

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
            collection.AddSingleton<IMeasureSensor>(x =>
            {
                var dispatcher = _serviceProvider.GetService<IDomainEventDispatcher>();
                return new Hcsr04Sensor(dispatcher, 23, 24);
            });
            collection.AddSingleton<IBuzzer>(x =>
            {
                return new PiezoBuzzerController(17);
            });

            collection.AddSingleton<IParkingSensorService>(x => {
                var converter = _serviceProvider.GetService<IDistanceToToneFrequencyConverter>();
                return new ParkingSensorService(converter);
            });

            var builder = new ContainerBuilder();

            builder.Populate(collection);

            var coreAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(DomainEventDispatcher));
            builder.RegisterAssemblyTypes(coreAssembly, infrastructureAssembly).AsImplementedInterfaces();

            var appContainer = builder.Build();
            _serviceProvider = new AutofacServiceProvider(appContainer);
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
