using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using WorldCapitalsAndWeather.Models;
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
                .UseStartup<TestStartup>()
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

        [Fact]
        public async void ReturnsTheCorrectCapital()
        {
            var response = await _client.GetAsync("/api/country/mexico");

            var asString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var country = JsonConvert.DeserializeObject<CountryResponse>(asString);

            Assert.Equal("Mexico City", country.Capital);
        }

        [Fact]
        public async void FailsWhenCountryIsNotFound()
        {
            var response = await _client.GetAsync("/api/country/invalid");

            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }

        [Fact]
        public async void ReturnsTheCorrectWeather()
        {
            var response = await _client.GetAsync("/api/country/cuba");

            var asString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var country = JsonConvert.DeserializeObject<CountryResponse>(asString);

            Assert.Equal("87.0", country.Temperature);
        }
    }
}
