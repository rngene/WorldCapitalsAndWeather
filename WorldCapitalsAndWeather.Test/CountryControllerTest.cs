using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
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
                .UseStartup<Startup>());

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
    }
}
