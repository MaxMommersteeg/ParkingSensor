using App.Core.Devices;

namespace App.Infrastructure.Devices
{
    public class SimulatedMeasureSensor : IMeasureSensor
    {
        private static readonly int[] _distances = new[] { 180, 160, 140, 120, 100, 80, 60 };
        private int _index = 0;

        public double GetDistance()
        {
            var distance = _distances[_index];

            UpdateIndex();

            return distance;
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

        public void Dispose()
        {
        }
    }
}
