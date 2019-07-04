using System;

namespace App.Core.Sensors
{
    public interface IBuzzer : IDevice
    {
        void PlayTone(double frequency, TimeSpan duration);

        void StartPlaying(double frequency);

        void StopPlaying();
    }
}
