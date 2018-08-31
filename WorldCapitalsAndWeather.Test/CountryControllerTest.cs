using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
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
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartup>()
                .UseContentRoot(GetContentRootPath())
            );

            _client = server.CreateClient();
        }

        private string GetContentRootPath()
        {
            var currentPath = Environment.CurrentDirectory;
            var contentPath = Path.Combine(currentPath, "../../../../WorldCapitalsAndWeather");

            return contentPath;
        }

        [Fact]
        public async void ReturnsSuccessWhenCountryIsValid()
        {
            var response = await _client.GetAsync("/api/country/usa");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void FailsWhenCountryIsInvalid()
        {
            var response = await _client.GetAsync("/api/country/invalid");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void ReturnsTheCorrectCapital()
        {
            var response = await _client.GetAsync("/api/country/colombia");

            var asString = await response.Content.ReadAsStringAsync();

            var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(asString);

            Assert.Equal("Bogota", countryResponse.Capital);

        }

        [Fact]
        public async void ReturnsTheCorrectTemperature()
        {
            var response = await _client.GetAsync("/api/country/cuba");

            var asString = await response.Content.ReadAsStringAsync();

            var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(asString);

            Assert.Equal("87.0", countryResponse.Temperature);

        }
    }
}
