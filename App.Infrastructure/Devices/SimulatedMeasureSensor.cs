using App.Core.Devices;

namespace App.Infrastructure.Devices
{
    public class SimulatedMeasureSensor : IMeasureSensor
    {
        private static readonly int[] _distances = new[] { 150, 140, 130, 120, 110, 100, 90, 80, 70, 60 };
        private int _index = 0;

        public double GetDistance()
        {
            var distance = _distances[_index];

            UpdateIndex();

            return distance;
        }

        public void Dispose()
        {
        }

        private void UpdateIndex()
        {
            if (_index == _distances.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
        }
    }
}
