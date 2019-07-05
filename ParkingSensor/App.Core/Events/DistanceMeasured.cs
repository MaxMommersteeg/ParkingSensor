using System;
using App.Core.Entities;
using App.Core.SharedKernel;

namespace App.Core.Events
{
    public class DistanceMeasured : BaseDomainEvent
    {
        public DistanceMeasured(DistanceMeasurement distanceMeasurement)
        {
            DistanceMeasurement = distanceMeasurement ?? throw new ArgumentNullException(nameof(distanceMeasurement));
        }

        public DistanceMeasurement DistanceMeasurement { get; }
    }
}
