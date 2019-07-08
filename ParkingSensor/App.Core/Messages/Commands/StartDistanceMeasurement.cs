using System;

namespace App.Core.Messages.Commands
{
    public class StartDistanceMeasurement : BaseCommand
    {
        public StartDistanceMeasurement(TimeSpan? measureInterval = null)
        {
            MeasureInterval = measureInterval;
        }

        public TimeSpan? MeasureInterval { get; }
    }
}
