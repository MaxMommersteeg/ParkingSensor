using App.Core.Devices;
using Iot.Device.Hcsr04;

namespace App.Infrastructure.Sensors
{
    // Docs: https://github.com/dotnet/iot/tree/master/src/devices/Hcsr04
    public class Hcsr04Sensor : IMeasureSensor
    {
        private readonly Hcsr04 _sensor;

        public Hcsr04Sensor(int triggerPin, int echoPin)
        {
            _sensor = new Hcsr04(triggerPin, echoPin);
        }

        public double GetDistance()
        {
            return _sensor.Distance;
        }

        public void Dispose()
        {
            _sensor?.Dispose();
        }
    }
}
