using System;
using System.Threading;
using App.Core.Devices;
using App.Core.Messages.Events;
using Iot.Device.Hcsr04;
using MediatR;

namespace App.Infrastructure.Sensors
{
    // Docs: https://github.com/dotnet/iot/tree/master/src/devices/Hcsr04
    public class Hcsr04Sensor : IMeasureSensor
    {
        private static readonly TimeSpan MinimalInterval = TimeSpan.FromMilliseconds(60);

        private readonly IMediator _messagingMediator;
        private readonly Hcsr04 _sensor;
        private readonly TimeSpan _measureInterval;

        private Timer _timer;

        public Hcsr04Sensor(IMediator messagingMediator, int triggerPin, int echoPin, TimeSpan? measureInterval = null)
        {
            _messagingMediator = messagingMediator;
            _sensor = new Hcsr04(triggerPin, echoPin);
            _measureInterval = GetValueOrMinimalInterval(measureInterval);
        }

        public void Start()
        {
            _timer = new Timer(OnIntervalElapsed, null, TimeSpan.Zero, _measureInterval);
        }

        private void OnIntervalElapsed(object state)
        {
            var distance = _sensor.Distance;
            _messagingMediator.Publish(new DistanceMeasured(distance));
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
