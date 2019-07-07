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

        public ParkingSensorService(
            IDistanceToToneFrequencyConverter distanceToToneFrequencyConverter)
        {
            _distanceToToneFrequencyConverter = distanceToToneFrequencyConverter;
        }

        public void Handle(DistanceMeasured @event)
        {
            if (!Started)
            {
                return;
            }

            var frequencyToPlay = _distanceToToneFrequencyConverter.DistanceToFrequency(@event.DistanceMeasurement.Distance);
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
        }
    }
}
