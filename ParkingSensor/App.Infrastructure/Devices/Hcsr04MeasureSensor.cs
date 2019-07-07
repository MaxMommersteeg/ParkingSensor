using System;
using System.Threading;
using App.Core.Devices;
using App.Core.Entities;
using App.Core.EventModels;
using App.Core.Interfaces;
using Iot.Device.Hcsr04;

namespace App.Infrastructure.Sensors
{
    // Docs: https://github.com/dotnet/iot/tree/master/src/devices/Hcsr04
    public class Hcsr04Sensor : IMeasureSensor
    {
        private static readonly TimeSpan MinimalInterval = TimeSpan.FromMilliseconds(60);

        private readonly IDomainEventDispatcher _dispatcher;
        private readonly Hcsr04 _sensor;
        private readonly TimeSpan _measureInterval;

        private Timer _timer;

        public Hcsr04Sensor(IDomainEventDispatcher dispatcher, int triggerPin, int echoPin, TimeSpan? measureInterval = null)
        {
            _dispatcher = dispatcher;
            _sensor = new Hcsr04(triggerPin, echoPin);
            _measureInterval = GetValueOrMinimalInterval(measureInterval);
        }

        public void Start()
        {
            _timer = new Timer(OnIntervalElapsed, null, TimeSpan.Zero, _measureInterval);
        }

        private void OnDistanceMeasured(DistanceMeasuredEventArgs distanceMeasuredEventArgs)
        {
            var distanceMeasurement = new DistanceMeasurement(distanceMeasuredEventArgs.Distance);
            distanceMeasurement.MarkMeasured();

            foreach (var @event in distanceMeasurement.Events)
            {
                _dispatcher.Dispatch(@event);
            }
        }

        private void OnIntervalElapsed(object state)
        {
            var distanceMeasuredEventArgs = new DistanceMeasuredEventArgs(_sensor.Distance);
            OnDistanceMeasured(distanceMeasuredEventArgs);
        }

        private TimeSpan GetValueOrMinimalInterval(TimeSpan? timeSpan)
        {
            if (!timeSpan.HasValue)
            {
                return MinimalInterval;
            }

            return timeSpan.Value >= MinimalInterval ? timeSpan.Value : MinimalInterval;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _sensor?.Dispose();
        }
    }
}
