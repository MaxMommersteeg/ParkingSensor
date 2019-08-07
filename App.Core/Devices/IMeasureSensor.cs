namespace App.Core.Devices
{
    public interface IMeasureSensor : IDevice
    {
        double GetDistance();
    }
}
