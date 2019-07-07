namespace App.Core.Messages.Events
{
    public class DistanceMeasured : BaseEvent
    {
        public DistanceMeasured(double distance)
        {
            Distance = distance;
        }

        public double Distance { get; }
    }
}
