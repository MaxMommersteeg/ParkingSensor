using System;

namespace App.Core.Model
{
    public class SoundEffect
    {
        public SoundEffect(double frequency, TimeSpan duration, int beeps)
        {
            Frequency = frequency;
            Duration = duration;
            Beeps = beeps;
        }

        public double Frequency { get; }

        public TimeSpan Duration { get; }

        public int Beeps { get; }
    }
}
