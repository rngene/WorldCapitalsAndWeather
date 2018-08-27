using WorldCapitalsAndWeather.Models;

namespace WorldCapitalsAndWeather.Services
{
    public interface ICountriesService
    {
        Country GetCountry(string name);
    }
}
