using System;
using App.Core;
using App.Core.DomainServices;
using App.Infrastructure.Sensors;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Started Parking Sensor app.");
            var distanceToToneFrequencyConverter = new DistanceToToneFrequencyConverter();
            var distanceSensor = new Hcsr04Sensor(23, 24);
            var buzzer = new PiezoBuzzerController(17);
            var parkingSensorService = new ParkingSensorService(
                distanceToToneFrequencyConverter,
                distanceSensor,
                buzzer);

            parkingSensorService.Start();

            Console.ReadKey();
        }
    }
}
