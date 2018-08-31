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
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetWeather(string regionId)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string temperature;
            try
            {
                var response = await _httpClient.GetAsync("http://dataservice.accuweather.com/currentconditions/v1/" + regionId + "?apikey=5qKUHLxRd4xixs30yD4Cr7PGDkvXOB0X").ConfigureAwait(false);
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
