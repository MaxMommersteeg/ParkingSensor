namespace App.Core.Messages.Commands
{
    public class RegisterBuzzer : BaseCommand
    {
        public RegisterBuzzer(int pinNumber)
        {
            PinNumber = pinNumber;
        }

        public int PinNumber { get; }
    }
}
