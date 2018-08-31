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
            var testServer = new TestServer(new WebHostBuilder()
                .UseEnvironment("Test")
                .UseContentRoot(GetContentRoot())
                .UseStartup<TestStartup>()
            );

            _client = testServer.CreateClient();
        }

        private string GetContentRoot()
        {
            var currentFolder = Environment.CurrentDirectory;
            return Path.Combine(currentFolder, "../../../../WorldCapitalsAndWeather");
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

            var responseAsString = await response.Content.ReadAsStringAsync();
            var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(responseAsString);
            
            Assert.Equal("Bogota", countryResponse.Capital);
        }

        [Fact]
        public async void ReturnsTheCorrectWeather()
        {
            var response = await _client.GetAsync("/api/country/cuba");

            var responseAsString = await response.Content.ReadAsStringAsync();
            var countryResponse = JsonConvert.DeserializeObject<CountryResponse>(responseAsString);

            Assert.Equal("87.0", countryResponse.Temperature);
        }
    }
}
