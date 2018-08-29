using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace WorldCapitalsAndWeather.Test
{
    public class CountryControllerTest
    {
        private readonly HttpClient _client;

        public CountryControllerTest()
        {
            var testServer = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<Startup>()
                .UseContentRoot(GetContentRoot()));

            _client = testServer.CreateClient();
        }

        private string GetContentRoot()
        {
            var currentFolder = Environment.CurrentDirectory;
            var root = Path.Combine(currentFolder, @"..\..\..\..\WorldCapitalsAndWeather");

            return root;
        }

        [Fact]
        public async void ReturnsOkWhenCountryExists()
        {
            var response = await _client.GetAsync("/api/country/usa");

            response.EnsureSuccessStatusCode();
        }
    }
}
