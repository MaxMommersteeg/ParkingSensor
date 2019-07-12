using App.Core.Devices;
using Iot.Device.Buzzer;

namespace App.Infrastructure.Sensors
{
    // Docs: https://github.com/dotnet/iot/blob/master/src/devices/Buzzer/README.md
    public class PiezoBuzzerController : IBuzzer
    {
        private const int ForceSoftwarePWM = -1;

        private readonly Buzzer _buzzer;

        private bool _isPlaying;
        private double _currentFrequency;

        public PiezoBuzzerController(int pinNumber)
        {
            _buzzer = new Buzzer(pinNumber, ForceSoftwarePWM);
        }

        public void StartPlaying(double frequency)
        {
            if (frequency == _currentFrequency)
            {
                return;
            }

            _buzzer.SetFrequency(frequency);
            _currentFrequency = frequency;
            _isPlaying = true;
        }

        public void StopPlaying()
        {
            if (!_isPlaying)
            {
                return;
            }

            _buzzer.StopPlaying();
            _isPlaying = false;
        }

        public void Dispose()
        {
            _buzzer?.Dispose();
        }
    }
}
