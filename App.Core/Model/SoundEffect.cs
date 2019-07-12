using System;

namespace App.Core.Model
{
    public class SoundEffect
    {
        public SoundEffect(double frequency)
        {
            Frequency = frequency;
        }

        public double Frequency { get; }
    }
}
