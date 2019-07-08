using System;
using App.Core.Devices;

namespace App.Fakes
{
    public class FakeBuzzer : IBuzzer
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void PlayTone(double frequency, TimeSpan duration)
        {
        }

        public void StartPlaying(double frequency)
        {
        }

        public void StopPlaying()
        {
        }
    }
}
