using System;

namespace App.Core.Messages.Commands
{
    public class StartSoundEffect : BaseCommand
    {
        public StartSoundEffect(double frequency)
        {
            Frequency = frequency;
        }

        public double Frequency { get; }
    }
}
