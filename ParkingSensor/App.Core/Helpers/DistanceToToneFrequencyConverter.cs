using App.Core.Helpers;
using System.Collections.Generic;

namespace App.Core
{
    public class DistanceToToneFrequencyConverter : IDistanceToToneFrequencyConverter
    {
        private static readonly IDictionary<Tone, double> _toneFrequencies = new Dictionary<Tone, double>
        {
            [Tone.None] = 0,
            [Tone.Lowest] = 404,
            [Tone.Low] = 504,
            [Tone.Medium] = 604,
            [Tone.Higher] = 704,
            [Tone.Highest] = 804
        };

        private static double GetFrequency(Tone tone)
        {
            return _toneFrequencies[tone];
        }

        public double DistanceToFrequency(double distanceInCentimeter)
        {
            if (distanceInCentimeter >= 140)
            {
                return GetFrequency(Tone.None);
            }

            if (distanceInCentimeter >= 120 && distanceInCentimeter < 140)
            {
                return GetFrequency(Tone.Lowest);
            }

            if (distanceInCentimeter >= 110 && distanceInCentimeter < 120)
            {
                return GetFrequency(Tone.Low);
            }

            if (distanceInCentimeter >= 100 && distanceInCentimeter < 110)
            {
                return GetFrequency(Tone.Low);
            }

            if (distanceInCentimeter >= 90 && distanceInCentimeter < 100)
            {
                return GetFrequency(Tone.Medium);
            }

            if (distanceInCentimeter >= 80 && distanceInCentimeter < 90)
            {
                return GetFrequency(Tone.Higher);
            }

            if (distanceInCentimeter >= 70 && distanceInCentimeter < 80)
            {
                return GetFrequency(Tone.Higher);
            }

            return GetFrequency(Tone.Highest);
        }
    }
}
