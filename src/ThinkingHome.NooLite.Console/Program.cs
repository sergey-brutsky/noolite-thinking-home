namespace ThinkingHome.NooLite.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var gateway = new PR1132Gateway("192.168.2.3");

            foreach (var sensorData in gateway.LoadSensorData())
            {
                System.Console.WriteLine($"State: {sensorData.State}, Temp: {sensorData.Temperature}, Humidity: {sensorData.Humidity}");
                
            }

            System.Console.ReadKey();
        }
    }
}
