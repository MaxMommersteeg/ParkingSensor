using App.Core.Model;

namespace App.Core
{
    public interface IDistanceToSoundEffectConverter
    {
        SoundEffect DistanceToSoundEffect(double distanceInCentimeters);
    }
}
