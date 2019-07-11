using System;
using App.Core.Devices;
using Iot.Device.Buzzer;

namespace App.Infrastructure.Sensors
{
    // Docs: https://github.com/dotnet/iot/blob/master/src/devices/Buzzer/README.md
    public class PiezoBuzzerController : IBuzzer
    {
        private const int ForceSoftwarePWM = -1;

        private readonly Buzzer _buzzer;

        public PiezoBuzzerController(int pinNumber)
        {
            _buzzer = new Buzzer(pinNumber, ForceSoftwarePWM);
        }

        public void PlaySoundEffect(double frequency, TimeSpan duration, int beeps)
        {
            var toneDurationInMilliseconds = Convert.ToInt32(duration.TotalMilliseconds / beeps);
            for (var i = 0; i < beeps; i++)
            {
                _buzzer.PlayTone(frequency, toneDurationInMilliseconds);
            }
        }

        public void StartPlaying(double frequency)
        {
            _buzzer.SetFrequency(frequency);
        }

        public void StopPlaying()
        {
            _buzzer.StopPlaying();
        }

        public void Dispose()
        {
            _buzzer?.Dispose();
        }
    }
}
