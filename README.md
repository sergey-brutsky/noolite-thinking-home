# Noolite Thinking Home
This is a C# library that provides API for [Noolite gateway](https://www.noo.com.by/Ethernet_PR1132.html)

**Warning**: Now only temperature and humidity sensors supported

Example of usage 

```csharp
public class Program
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
```
