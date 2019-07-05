using App.Core.Entities;
using App.Core.Helpers;

namespace App.Core
{
    public class DistanceToToneFrequencyConverter : IDistanceToToneFrequencyConverter
    {
        public double DistanceToFrequency(double distanceInCentimeter)
        {
            if (distanceInCentimeter >= 140)
            {
                return ToneFrequencies.GetFrequency(Tone.None);
            }

            if (distanceInCentimeter >= 120 && distanceInCentimeter < 140)
            {
                return ToneFrequencies.GetFrequency(Tone.Lowest);
            }

            if (distanceInCentimeter >= 110 && distanceInCentimeter < 120)
            {
                return ToneFrequencies.GetFrequency(Tone.Low);
            }

            if (distanceInCentimeter >= 100 && distanceInCentimeter < 110)
            {
                return ToneFrequencies.GetFrequency(Tone.Low);
            }

            if (distanceInCentimeter >= 90 && distanceInCentimeter < 100)
            {
                return ToneFrequencies.GetFrequency(Tone.Medium);
            }

            if (distanceInCentimeter >= 80 && distanceInCentimeter < 90)
            {
                return ToneFrequencies.GetFrequency(Tone.Higher);
            }

            if (distanceInCentimeter >= 70 && distanceInCentimeter < 80)
            {
                return ToneFrequencies.GetFrequency(Tone.Higher);
            }

            return ToneFrequencies.GetFrequency(Tone.Highest);
        }
    }
}
