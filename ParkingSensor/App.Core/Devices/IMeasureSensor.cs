using System;

namespace App.Core.Devices
{
    public interface IMeasureSensor<T>: IDevice
        where T : class
    {
        event EventHandler<T> ValueMeasured;
    }
}
