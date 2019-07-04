using System;

namespace App.Infrastructure.EventModels
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
