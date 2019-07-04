using App.Infrastructure.Sensors;
using System;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Started Parking Sensor app.");
            var sensor = new Hcsr04Sensor(14, 2);
            sensor.ValueMeasured += OnSensorValueMeasured;
        }

        private static void OnSensorValueMeasured(object sender, Infrastructure.EventModels.DistanceMeasuredEventArgs e)
        {
        }
    }
}
