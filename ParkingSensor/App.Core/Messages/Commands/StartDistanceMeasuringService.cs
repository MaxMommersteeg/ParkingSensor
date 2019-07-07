namespace App.Core.Messages.Commands
{
    public class StartDistanceMeasuringService : BaseCommand
    {
        public StartDistanceMeasuringService(int triggerPin, int echoPin)
        {
            TriggerPin = triggerPin;
            EchoPin = echoPin;
        }

        public int TriggerPin { get; }

        public int EchoPin { get; }
    }
}
