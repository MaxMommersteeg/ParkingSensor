using App.Core.Sensors;
using Iot.Device.Buzzer;
using System;

namespace App.Infrastructure.Sensors
{
    public class PiezoBuzzerController : IBuzzer
    {
        private const int ForceSoftwarePWM = -1;

        private readonly Buzzer _buzzer;

        public PiezoBuzzerController(int pinNumber)
        {
            _buzzer = new Buzzer(pinNumber, ForceSoftwarePWM);
        }

        public void PlayTone(double frequency, TimeSpan duration)
        {
            _buzzer.PlayTone(frequency, (int)duration.TotalSeconds);
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
