using System;
using App.Core.EventModels;
using App.Infrastructure.Sensors;

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

        private static void OnSensorValueMeasured(object sender, DistanceMeasuredEventArgs e)
        {
        }
    }
}
