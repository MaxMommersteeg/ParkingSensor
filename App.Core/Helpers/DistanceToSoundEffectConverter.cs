using App.Core.Model;

namespace App.Core
{
    public class DistanceToSoundEffectConverter : IDistanceToSoundEffectConverter
    {
        public SoundEffect DistanceToSoundEffect(double distanceInCentimeters)
        {
            if (distanceInCentimeters >= 140)
            {
                return null;
            }

            if (distanceInCentimeters >= 130 && distanceInCentimeters < 140)
            {
                return new SoundEffect(600);
            }

            if (distanceInCentimeters >= 120 && distanceInCentimeters < 130)
            {
                return new SoundEffect(750);
            }

            if (distanceInCentimeters >= 110 && distanceInCentimeters < 120)
            {
                return new SoundEffect(900);
            }

            if (distanceInCentimeters >= 100 && distanceInCentimeters < 110)
            {
                return new SoundEffect(1050);
            }

            if (distanceInCentimeters >= 90 && distanceInCentimeters < 100)
            {
                return new SoundEffect(1200);
            }

            if (distanceInCentimeters >= 80 && distanceInCentimeters < 90)
            {
                return new SoundEffect(1350);
            }

            if (distanceInCentimeters >= 70 && distanceInCentimeters < 80)
            {
                return new SoundEffect(1500);
            }

            return new SoundEffect(1650);
        }
    }
}
