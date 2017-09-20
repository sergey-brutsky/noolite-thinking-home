using System;
using ThinkingHome.NooLite;

namespace ThinkingHome.NooliteConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var gateway = new Pr1132Gateway("192.168.2.3"))
            {
                foreach (var sensorData in gateway.LoadSensorData())
                {
                    Console.WriteLine($"State: {sensorData.State}, Temp: {sensorData.Temperature}, Humidity: {sensorData.Humidity}");
                }

                Console.ReadKey();
            }
        }
    }
}
