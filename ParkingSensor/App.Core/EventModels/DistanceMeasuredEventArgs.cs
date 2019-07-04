using System;

namespace App.Core.EventModels
{
    public class DistanceMeasuredEventArgs : EventArgs
    {
        public DistanceMeasuredEventArgs(double distance)
        {
            Distance = distance;
        }

        public double Distance { get; }
    }
}
