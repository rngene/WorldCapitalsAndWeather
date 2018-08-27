using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorldCapitalsAndWeather.Models;

namespace WorldCapitalsAndWeather.Services
{
    public class WeatherService:IWeatherService
    {

        public async Task<string> GetWeather(string regionId)
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.Proxy = new CarnivalProxy();
            HttpClient client = new HttpClient(clientHandler);
            //client.BaseAddress = new Uri("http://dataservice.accuweather.com/currentconditions/v1/" + regionId + "?apikey=dtWdRsvOQaWb7QRTiVA8Hdvrsmwtck7G");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string temperature;
            try
            {
                HttpResponseMessage response = await client.GetAsync("").ConfigureAwait(false);
                var asString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var weather = JsonConvert.DeserializeObject<AccuweatherResult[]>(asString);
                temperature = weather[0].Temperature.Imperial.Value.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
                temperature = "N/A";
            }

            return temperature;
        }
    }
}
