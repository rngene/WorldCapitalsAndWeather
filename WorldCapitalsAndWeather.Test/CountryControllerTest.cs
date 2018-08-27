using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using WorldCapitalsAndWeather.Models;
using Xunit;

namespace WorldCapitalsAndWeather.Test
{
    public class CountryControllerTest
    {
        private readonly TestServer _testServer;
        protected HttpClient _client;
        private HttpRequestMessage _request;

        public CountryControllerTest()
        {
            _testServer = new TestServer(new WebHostBuilder()
                .UseContentRoot(GetContentRootPath())
                .UseEnvironment("Test")
                .UseStartup<TestStartup>());

            _client = _testServer.CreateClient();
        }

        private string GetContentRootPath()
        {
            string testProjectPath = Directory.GetCurrentDirectory();
            var relativePathToWebProject = @"..\..\..\..\WorldCapitalsAndWeather";
            var result = Path.Combine(testProjectPath, relativePathToWebProject);
            return result;
        }

        [Fact]
        public async Task ReturnsOkWhenCountryExists()
        {
            _request = new HttpRequestMessage(HttpMethod.Get,"/api/country/usa");

            var response = await _client.SendAsync(_request);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ReturnsTheCorrectCapital()
        {
            _request = new HttpRequestMessage(HttpMethod.Get, "/api/country/argentina");
            var response = await _client.SendAsync(_request);

            var responseString = await response.Content.ReadAsStringAsync();
            var countryResponse = JsonConvert.DeserializeObject<CountryViewModel>(responseString);
            Assert.Equal("Buenos Aires", countryResponse.Capital);
        }

        [Fact]
        public async Task FailsWhenCountryIsNotFound()
        {
            _request = new HttpRequestMessage(HttpMethod.Get, "/api/country/invalid");
            var response = await _client.SendAsync(_request);

            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }

        [Fact]
        public async Task ReturnsTheCorrectWeather()
        {
            _request = new HttpRequestMessage(HttpMethod.Get, "/api/country/usa");
            var response = await _client.SendAsync(_request);

            var responseString = await response.Content.ReadAsStringAsync();
            var countryResponse = JsonConvert.DeserializeObject<CountryViewModel>(responseString);
            Assert.Equal("87", countryResponse.Temperature);
        }
    }
}
