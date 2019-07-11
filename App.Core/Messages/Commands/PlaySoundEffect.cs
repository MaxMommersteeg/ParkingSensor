using System;

namespace App.Core.Messages.Commands
{
    public class PlaySoundEffect : BaseCommand
    {
        public PlaySoundEffect(double frequency, TimeSpan duration, int beeps)
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
