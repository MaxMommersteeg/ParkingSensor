using System;
using App.Core.Devices;

namespace App.Infrastructure.Devices
{
    public class SimulatedBuzzer : IBuzzer
    {
        public void PlayTone(double frequency, TimeSpan duration)
        {
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
    }
}
