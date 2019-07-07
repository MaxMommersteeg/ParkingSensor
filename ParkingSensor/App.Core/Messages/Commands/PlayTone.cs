using System;

namespace App.Core.Messages.Commands
{
    public class PlayTone : BaseCommand
    {
        public PlayTone(double frequency, TimeSpan duration)
        {
            Frequency = frequency;
            Duration = duration;
        }

        public double Frequency { get; }

        public TimeSpan Duration { get; }
    }
}
