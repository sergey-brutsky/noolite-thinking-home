namespace ThinkingHome.NooLite.WebServer
{
    internal class SensorData
    {
        public SensorData(decimal? temperature, int? humidity)
        {
            t = temperature;
            h = humidity;
        }

        public decimal? t { get; }
        public int? h { get;  }
    }
}