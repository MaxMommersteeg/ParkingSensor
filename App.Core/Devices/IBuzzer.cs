using System;

namespace App.Core.Devices
{
    public interface IBuzzer : IDevice
    {
        void PlaySoundEffect(double frequency, TimeSpan duration, int beeps);

        void StartPlaying(double frequency);

        void StopPlaying();
    }
}
