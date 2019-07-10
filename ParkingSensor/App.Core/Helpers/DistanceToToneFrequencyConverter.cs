using App.Core.Helpers;
using System.Collections.Generic;

namespace App.Core
{
    public class DistanceToToneFrequencyConverter : IDistanceToToneFrequencyConverter
    {
        private static readonly IDictionary<Tone, double> _toneFrequencies = new Dictionary<Tone, double>
        {
            [Tone.None] = 0,
            [Tone.Lowest] = 800,
            [Tone.Low] = 1000,
            [Tone.Medium] = 1200,
            [Tone.Higher] = 1400,
            [Tone.Highest] = 1600
        };

        private static double GetFrequency(Tone tone)
        {
            return _toneFrequencies[tone];
        }

        public double DistanceToFrequency(double distanceInCentimeters)
        {
            if (distanceInCentimeters >= 140)
            {
                return GetFrequency(Tone.None);
            }

            if (distanceInCentimeters >= 120 && distanceInCentimeters < 140)
            {
                return GetFrequency(Tone.Lowest);
            }

            if (distanceInCentimeters >= 110 && distanceInCentimeters < 120)
            {
                return GetFrequency(Tone.Low);
            }

            if (distanceInCentimeters >= 100 && distanceInCentimeters < 110)
            {
                return GetFrequency(Tone.Low);
            }

            if (distanceInCentimeters >= 90 && distanceInCentimeters < 100)
            {
                return GetFrequency(Tone.Medium);
            }

            if (distanceInCentimeters >= 80 && distanceInCentimeters < 90)
            {
                return GetFrequency(Tone.Higher);
            }

            if (distanceInCentimeters >= 70 && distanceInCentimeters < 80)
            {
                return GetFrequency(Tone.Higher);
            }

            return GetFrequency(Tone.Highest);
        }
    }
}
