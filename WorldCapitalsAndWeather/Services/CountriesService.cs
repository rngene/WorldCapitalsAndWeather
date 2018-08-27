using System.Linq;
using WorldCapitalsAndWeather.Models;

namespace WorldCapitalsAndWeather.Services
{
    public class CountriesService : ICountriesService
    {

        public Country GetCountry(string name)
        {
            var db = new CountriesContext();

            var country = db.Country.FirstOrDefault(c => c.Name == name);

            return country;

        }
    }
}
