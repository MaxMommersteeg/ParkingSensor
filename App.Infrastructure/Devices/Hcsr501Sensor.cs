using System;
using System.Device.Gpio;
using System.Threading.Tasks;
using App.Core.Devices;
using App.Core.Messages.Events;
using Iot.Device.Hcsr501;
using MediatR;

namespace App.Infrastructure.Sensors
{
    // Docs: https://github.com/dotnet/iot/tree/master/src/devices/Hcsr501
    public class Hcsr501Sensor : IMotionDetectionSensor
    {
        private readonly IMediator _mediator;
        private readonly Hcsr501 _sensor;

        public Hcsr501Sensor(
            IMediator mediator,
            int outPin)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            _sensor = new Hcsr501(outPin);
            _sensor.Hcsr501ValueChanged += Sensor_Hcsr501ValueChanged;
        }

        public void Dispose()
        {
            _sensor?.Dispose();
        }

        private void Sensor_Hcsr501ValueChanged(object sender, Hcsr501ValueChangedEventArgs e)
        {
            BaseEvent @event;
            if (e.PinValue == PinValue.High)
            {
                @event = new MotionDetected();
            }
            else
            {
                @event = new MotionStopped();
            }

            Task.Run(async () => await _mediator.Publish(@event));
        }
    }
}
