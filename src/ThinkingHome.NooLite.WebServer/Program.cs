using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ThinkingHome.NooLite.WebServer
{
    public class Program
    {
        private const string AppsettingsJson = "appsettings.json";
        private const string ApplicationJsonMediaType = "application/json";

        private static IConfigurationRoot _config;

        public static void Main(string[] args)
        {
            if (!File.Exists(AppsettingsJson))
            {
                Console.WriteLine($"File '{AppsettingsJson}' is not found in the current directory, but it's required");
                return;
            }

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");

            _config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(AppsettingsJson)
               .Build();
            
            new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://" + _config["listen"])
                .Configure(appbuilder => appbuilder.Run(async context =>
                 {
                     if (context.Request.Path == _config["sensors-url"])
                     {
                         var gateway = new PR1132Gateway(_config["gateway"]);

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
    }
}
