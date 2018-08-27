using System.Threading.Tasks;

namespace WorldCapitalsAndWeather.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeather(string regionId);
    }
}
