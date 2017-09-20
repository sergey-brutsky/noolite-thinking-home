# Noolite Thinking Home
This is a c# library that provides API for [Noolite gateway](https://www.noo.com.by/Ethernet_PR1132.html)

**Warning**: Now only temperature and humidity sensors supported

Example of usage 

```csharp
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
```
