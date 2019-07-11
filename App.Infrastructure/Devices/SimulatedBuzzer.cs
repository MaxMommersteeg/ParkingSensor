using System;
using System.Threading.Tasks;
using App.Core.Devices;

namespace App.Infrastructure.Devices
{
    public class SimulatedBuzzer : IBuzzer
    {
        private const int MinFrequency = 37;
        private const int MaxFrequency = 32767;
        private const int DefaultFrequency = 500;

        public void PlaySoundEffect(double frequency, TimeSpan duration, int beeps)
        {
            var toneDurationInMilliseconds = Convert.ToInt32(duration.TotalMilliseconds / beeps);

            Task.Run(() => 
            {
                for (var i = 0; i < beeps; i++)
                {
                    Console.Beep(GetValueOrDefaultFrequency(frequency), toneDurationInMilliseconds);
                }
            });
        }

        public void StartPlaying(double frequency)
        {
        }

        public void StopPlaying()
        {
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
