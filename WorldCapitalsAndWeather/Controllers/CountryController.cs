using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldCapitalsAndWeather.Models;
using WorldCapitalsAndWeather.Services;

namespace WorldCapitalsAndWeather.Controllers
{
    [Route("api/[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountriesService _countriesService;
        private readonly IWeatherService _weatherService;

        public CountryController(ICountriesService countriesService, IWeatherService weatherService)
        {
            _countriesService = countriesService;
            _weatherService = weatherService;
        }

        [HttpGet("{country}")]
        public async Task<IActionResult> Get([FromRoute]string country)
        {
            var countryDb =_countriesService.GetCountry(country);
            
            if (countryDb == null)
            {
                return NotFound();
            }

            var temperature = await _weatherService.GetWeather(countryDb.RegionId);
            return Ok(new CountryResponse {Capital = countryDb.Capital, Temperature = temperature});
        }
    }
}
