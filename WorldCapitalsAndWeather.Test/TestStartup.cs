using System;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RichardSzalay.MockHttp;
using WorldCapitalsAndWeather.Services;

namespace WorldCapitalsAndWeather.Test
{
    public class TestStartup : Startup
    {
        private static Lazy<string> _content = new Lazy<string>(() => File.ReadAllText(@"Data\Weather.json"), LazyThreadSafetyMode.ExecutionAndPublication);

        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureServices(services);

            //var weatherServiceMock = new Mock<IWeatherService>();
            //weatherServiceMock.Setup(w => w.GetWeather(It.IsAny<string>())).ReturnsAsync("87");

            //services.AddSingleton(weatherServiceMock.Object);

            //Using https://github.com/richardszalay/mockhttp to mock
            var mockHttp = new MockHttpMessageHandler();

            mockHttp.When("http://dataservice.accuweather.com/currentconditions/v1/*")
                .Respond(HttpStatusCode.OK, "application/json", _content.Value);

            var client = mockHttp.ToHttpClient();

            services.AddSingleton(client);
        }
    }
}
