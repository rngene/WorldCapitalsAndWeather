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
        private readonly static  Lazy<string> _weatherResponse = new Lazy<string>(() => File.ReadAllText(@"Data\weather.json"));

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureServices(services);

            //var weatherServiceMock = new Mock<IWeatherService>();
            //weatherServiceMock.Setup(w => w.GetWeather(It.IsAny<String>())).ReturnsAsync("87.0");

            //services.AddSingleton(weatherServiceMock.Object);

            var mockHttpHandler = new MockHttpMessageHandler();
            mockHttpHandler.When("http://dataservice.accuweather.com/currentconditions/v1/*")
                .Respond(HttpStatusCode.OK, "application/json", _weatherResponse.Value);

            services.AddSingleton(mockHttpHandler.ToHttpClient());

        }
    }
}
