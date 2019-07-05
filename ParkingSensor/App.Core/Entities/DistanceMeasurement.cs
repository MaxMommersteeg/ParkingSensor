using App.Core.Events;
using App.Core.SharedKernel;

namespace App.Core.Entities
{
    public class DistanceMeasurement : BaseEntity
    {
        public DistanceMeasurement(double distance)
        {
            Distance = distance;
        }

        public double Distance { get; private set; }

        public bool IsMeasured { get; private set; }


        public void MarkMeasured()
        {
            IsMeasured = true;
            Events.Add(new DistanceMeasured(this));
        }
    }
}
