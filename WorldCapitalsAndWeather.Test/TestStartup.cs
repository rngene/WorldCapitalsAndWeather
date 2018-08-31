using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RichardSzalay.MockHttp;
using WorldCapitalsAndWeather.Services;

namespace WorldCapitalsAndWeather.Test
{
    public class TestStartup: Startup
    {
        private static readonly Lazy<string> _weatherContent =
            new Lazy<string>(() => File.ReadAllText(@"Data\Weather.json"));

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureServices(services);

            //var weatherMock = new Mock<IWeatherService>();
            //weatherMock.Setup(w => w.GetWeather(It.IsAny<string>())).ReturnsAsync("87.0");

            //services.AddSingleton(weatherMock.Object);

            var mockhttp = new MockHttpMessageHandler();
            mockhttp.When("http://dataservice.accuweather.com/currentconditions/v1/*").Respond(
                HttpStatusCode.OK, "application/json", _weatherContent.Value);

            services.AddSingleton(mockhttp.ToHttpClient());
        }
    }
}
