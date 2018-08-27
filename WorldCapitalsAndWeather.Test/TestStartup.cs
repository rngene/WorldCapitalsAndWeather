using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WorldCapitalsAndWeather.Services;

namespace WorldCapitalsAndWeather.Test
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureServices(services);

            var weatherServiceMock = new Mock<IWeatherService>();
            weatherServiceMock.Setup(w => w.GetWeather(It.IsAny<string>())).ReturnsAsync("87");

            services.AddSingleton(weatherServiceMock.Object);
        }
    }
}
