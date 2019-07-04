﻿using System;
using System.Threading;
using App.Core.Sensors;
using App.Infrastructure.EventModels;
using Iot.Device.Hcsr04;

namespace App.Infrastructure.Sensors
{
    // Docs: https://components101.com/sites/default/files/component_datasheet/HCSR04%20Datasheet.pdf
    public class Hcsr04Sensor : IMeasureSensor<DistanceMeasuredEventArgs>
    {
        private static readonly TimeSpan MinimalInterval = TimeSpan.FromMilliseconds(60);

        private readonly Hcsr04 _sensor;
        private readonly TimeSpan _measureInterval;
        private readonly Timer _timer;

        public Hcsr04Sensor(int triggerPin, int echoPin, TimeSpan? measureInterval = null)
        {
            _sensor = new Hcsr04(triggerPin, echoPin);
            _measureInterval = GetValueOrMinimalInterval(measureInterval);
            _timer = new Timer(OnIntervalElapsed, null, TimeSpan.Zero, _measureInterval);
        }

        public event EventHandler<DistanceMeasuredEventArgs> ValueMeasured;
        private void OnDistanceMeasured(DistanceMeasuredEventArgs distanceMeasuredEventArgs)
        {
            ValueMeasured?.Invoke(this, distanceMeasuredEventArgs);
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
