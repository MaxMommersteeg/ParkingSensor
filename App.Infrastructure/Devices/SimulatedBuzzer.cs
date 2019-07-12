using System;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Devices;

namespace App.Infrastructure.Devices
{
    public class SimulatedBuzzer : IBuzzer
    {
        private const int MinFrequency = 37;
        private const int MaxFrequency = 32767;
        private const int DefaultFrequency = 500;
        private const int MinDurationInMilliseconds = 1;

        private bool _isPlaying;
        private double _lastPlayedFrequency;

        public void StartPlaying(double frequency)
        {
            if (frequency == _lastPlayedFrequency)
            {
                return;
            }

            Task.Run(() =>
            {
                Console.Beep(GetValueOrDefaultFrequency(frequency), Convert.ToInt32(TimeSpan.FromHours(1).TotalMilliseconds));
            });

            _isPlaying = true;
            _lastPlayedFrequency = frequency;
        }

        public void StopPlaying()
        {
            if (!_isPlaying)
            {
                return;
            }

            Console.Beep(MinFrequency, MinDurationInMilliseconds);
            _isPlaying = false;
        }

        public void Dispose()
        {
        }

        private static int GetValueOrDefaultFrequency(double frequency)
        {
            if (frequency < MinFrequency)
            {
                return DefaultFrequency;
            }

            if (frequency > MaxFrequency)
            {
                return DefaultFrequency;
            }

            return Convert.ToInt32(frequency);
        }
    }
}
