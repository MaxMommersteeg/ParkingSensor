using System;

namespace App.Core.Devices
{
    public interface IBuzzer : IDevice
    {
        void PlayTone(double frequency, TimeSpan duration);

        void StartPlaying(double frequency);

        void StopPlaying();
    }
}
