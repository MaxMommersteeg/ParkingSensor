using App.Core.Devices;
using App.Core.EventModels;
using System;

namespace App.Core.DomainServices
{
    public class ParkingSensorService : IParkingSensorService
    {
        private static readonly TimeSpan ToneDuration = TimeSpan.FromMilliseconds(50);

        private readonly IDistanceToToneFrequencyConverter _distanceToToneFrequencyConverter;
        private readonly IMeasureSensor<DistanceMeasuredEventArgs> _measureSensor;
        private readonly IBuzzer _buzzer;

        private bool _started;

        public ParkingSensorService(
            IDistanceToToneFrequencyConverter distanceToToneFrequencyConverter,
            IMeasureSensor<DistanceMeasuredEventArgs> measureSensor,
            IBuzzer buzzer)
        {
            _distanceToToneFrequencyConverter = distanceToToneFrequencyConverter;
            _measureSensor = measureSensor;
            _buzzer = buzzer;

            _measureSensor.ValueMeasured += OnSensorValueMeasured;
        }

        private void OnSensorValueMeasured(object sender, DistanceMeasuredEventArgs e)
        {
            var frequencyToPlay = _distanceToToneFrequencyConverter.DistanceToFrequency(e.Distance);
            _buzzer.PlayTone(frequencyToPlay, ToneDuration);
        }

        public bool Started => _started;

        public void Start()
        {
            _measureSensor.ValueMeasured += OnSensorValueMeasured;
            _started = true;
        }

        public void Stop()
        {
            _measureSensor.ValueMeasured -= OnSensorValueMeasured;
            _started = false;
        }

        public void Dispose()
        {
            _measureSensor?.Dispose();
            _buzzer?.Dispose();
        }
    }
}
