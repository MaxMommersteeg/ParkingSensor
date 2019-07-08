using App.Core.Devices;

namespace App.Fakes
{
    public class FakeMeasureSensor : IMeasureSensor
    {
        public double GetDistance()
        {
            return 10;
        }

        public void Dispose()
        {
        }
    }
}
