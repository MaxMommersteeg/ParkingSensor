using System.Collections.Generic;
using App.Core.Entities;

namespace App.Core.Helpers
{
    internal static class ToneFrequencies
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

        internal static double GetFrequency(Tone tone)
        {
            return _toneFrequencies[tone];
        }
    }
}
