using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ThinkingHome.NooLite;

namespace ThinkingHome.NooLiteWebServer
{
    public class Program
    {
        private const string AppsettingsJson = "appsettings.json";
        private const string ApplicationJsonMediaType = "application/json";

        private static IConfigurationRoot _config;

        public static void Main(string[] args)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppsettingsJson)
                .Build();

            new WebHostBuilder()
                .UseKestrel()
                .UseUrls(_config["listen"])
                .Configure(appbuilder => appbuilder.Run(async context =>
                {
                    if (context.Request.Path == _config["sensors-url"])
                    {
                        var gateway = new Pr1132Gateway(_config["gateway"]);

                        var serializedData = JsonConvert.SerializeObject(gateway.LoadSensorData().Select(x => new SensorData(x.Temperature, x.Humidity)));

                        context.Response.ContentType = ApplicationJsonMediaType;

                        await context.Response.WriteAsync(serializedData);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    }
                }))
                .Build()
                .Run();
        }

        internal class SensorData
        {
            public SensorData(float? temperature, int? humidity)
            {
                t = temperature;
                h = humidity;
            }

            public float? t { get; }
            public int? h { get; }
        }
    }
}
