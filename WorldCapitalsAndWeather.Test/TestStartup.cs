using System;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RichardSzalay.MockHttp;
using WorldCapitalsAndWeather.Services;

namespace WorldCapitalsAndWeather.Test
{
    public class TestStartup:Startup
    {
        private static readonly Lazy<string> _weatherContent = new Lazy<string>(() => File.ReadAllText(@"Data\Weather.json"));
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureServices(services);

            //var weatherServiceMock = new Mock<IWeatherService>();
            //weatherServiceMock.Setup(w => w.GetWeather(It.IsAny<string>())).ReturnsAsync("87.0");

            //services.AddSingleton(weatherServiceMock.Object);

            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://dataservice.accuweather.com/currentconditions/v1/*")
                .Respond(HttpStatusCode.OK, "application/json", _weatherContent.Value);

            services.AddSingleton(mockHttp.ToHttpClient());
        }
    }
}
