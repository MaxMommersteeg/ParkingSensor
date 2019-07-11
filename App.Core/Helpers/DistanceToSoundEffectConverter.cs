using App.Core.Model;
using System;

namespace App.Core
{
    public class DistanceToSoundEffectConverter : IDistanceToSoundEffectConverter
    {
        public SoundEffect DistanceToSoundEffect(double distanceInCentimeters)
        {
            if (distanceInCentimeters >= 140)
            {
                return new SoundEffect(0, TimeSpan.Zero, 0);
            }

            if (distanceInCentimeters >= 120 && distanceInCentimeters < 140)
            {
                return new SoundEffect(800, TimeSpan.FromSeconds(2), 1);
            }

            if (distanceInCentimeters >= 110 && distanceInCentimeters < 120)
            {
                return new SoundEffect(1000, TimeSpan.FromSeconds(2), 2);
            }

            if (distanceInCentimeters >= 100 && distanceInCentimeters < 110)
            {
                return new SoundEffect(1200, TimeSpan.FromSeconds(2), 3);
            }

            if (distanceInCentimeters >= 90 && distanceInCentimeters < 100)
            {
                return new SoundEffect(1400, TimeSpan.FromSeconds(2), 4);
            }

            if (distanceInCentimeters >= 80 && distanceInCentimeters < 90)
            {
                return new SoundEffect(1400, TimeSpan.FromSeconds(2), 4);
            }

            if (distanceInCentimeters >= 70 && distanceInCentimeters < 80)
            {
                return new SoundEffect(1600, TimeSpan.FromSeconds(2), 5);
            }

            return new SoundEffect(1800, TimeSpan.FromSeconds(2), 6);
        }
    }
}
