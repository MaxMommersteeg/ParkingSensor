using System;

namespace App.Core.Sensors
{
    public interface IMeasureSensor<T>: IDevice
        where T : class
    {
        event EventHandler<T> ValueMeasured;
    }
}
