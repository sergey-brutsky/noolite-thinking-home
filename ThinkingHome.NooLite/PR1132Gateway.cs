using System;
using System.Globalization;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ThinkingHome.NooLite
{
    public class Pr1132Gateway : IDisposable
    {
        private const string SensorDataFile = "sens.xml";
        private readonly HttpClient _client = new HttpClient(new HttpClientHandler { UseProxy = false });

        public Uri Host { get; }

        public Pr1132Gateway(string host)
        {
            Host = new Uri("http://" + host);
        }

        public Pr1132SensorData[] LoadSensorData()
        {
            var doc = XDocument.Parse(_client.GetStringAsync(new Uri(Host, SensorDataFile)).Result);

            var result = new Pr1132SensorData[4];

            for (var i = 0; i < 4; i++)
            {
                var strT = doc.XPathSelectElement("response/snst" + i).Value;
                var strH = doc.XPathSelectElement("response/snsh" + i).Value;
                var strState = doc.XPathSelectElement("response/snt" + i).Value;

                var data = new Pr1132SensorData { State = (SensorState)Convert.ToInt32(strState) };

                if (!string.IsNullOrEmpty(strT)) strT = strT.Replace(",", ".");

                if (float.TryParse(strT, NumberStyles.Any, CultureInfo.InvariantCulture, out float t)) data.Temperature = t;

                if (int.TryParse(strH, out int h)) data.Humidity = h;

                result[i] = data;
            }

            return result;
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
