namespace App.Core.Devices
{
    public interface IBuzzer : IDevice
    {
        void StartPlaying(double frequency);

        void StopPlaying();
    }
}
