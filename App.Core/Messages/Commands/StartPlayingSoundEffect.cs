using System;

namespace App.Core.Messages.Commands
{
    public class StartPlayingSoundEffect : BaseCommand
    {
        public StartPlayingSoundEffect(double frequency)
        {
            Frequency = frequency;
        }

        public double Frequency { get; }
    }
}
