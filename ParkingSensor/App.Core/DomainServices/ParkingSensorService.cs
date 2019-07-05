using App.Core.Devices;
using App.Core.Events;
using App.Core.Interfaces;
using System;

namespace App.Core.DomainServices
{
    public class ParkingSensorService : 
        IHandle<DistanceMeasured>,
        IParkingSensorService
    {
        private static readonly TimeSpan ToneDuration = TimeSpan.FromMilliseconds(50);

        private readonly IDistanceToToneFrequencyConverter _distanceToToneFrequencyConverter;
        private readonly IMeasureSensor _measureSensor;
        private readonly IBuzzer _buzzer;

        public ParkingSensorService(
            IDistanceToToneFrequencyConverter distanceToToneFrequencyConverter,
            IMeasureSensor measureSensor,
            IBuzzer buzzer)
        {
            _distanceToToneFrequencyConverter = distanceToToneFrequencyConverter;
            _measureSensor = measureSensor;
            _buzzer = buzzer;
        }

        public void Handle(DistanceMeasured @event)
        {
            if (!Started)
            {
                return;
            }

            var frequencyToPlay = _distanceToToneFrequencyConverter.DistanceToFrequency(@event.DistanceMeasurement.Distance);
            _buzzer.PlayTone(frequencyToPlay, ToneDuration);
        }

        public bool Started { get; private set; }

        public void Start()
        {
            Started = true;
        }

        public void Stop()
        {
            Started = false;
        }

        public void Dispose()
        {
            _measureSensor?.Dispose();
            _buzzer?.Dispose();
        }
    }
}
