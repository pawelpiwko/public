using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WordProcessingAPI.UnitTests
{
    public class WebServerTests
    {
        [Theory]
        [InlineData("XML", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        [InlineData("CSV", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        public async Task WordProcrssingControllerUrlPathTest_Success(string convertMethodString, string stringToConvert)
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .UseApplicationInsights();

            TestServer server = new TestServer(builder);

            HttpClient client = server.CreateClient();
            
            HttpContent data = new StringContent(JsonConvert.SerializeObject(stringToConvert), Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync("/api/wordprocessing/" + convertMethodString, data);
            
            response.EnsureSuccessStatusCode();

            string res = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("JPG", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        [InlineData("", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        public async Task WordProcrssingControllerUrlPathTest_Fail(string convertMethodString, string stringToConvert)
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .UseApplicationInsights();

            TestServer server = new TestServer(builder);

            HttpClient client = server.CreateClient();

            HttpContent data = new StringContent(JsonConvert.SerializeObject(stringToConvert), Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync("/api/wordprocessing/" + convertMethodString, data);

            string res = await response.Content.ReadAsStringAsync();

            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData("XML", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        [InlineData("CSV", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        public async Task WordProcrssingControllerObjectRequestTest_Success(string convertMethodString, string stringToConvert)
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .UseApplicationInsights();

            TestServer server = new TestServer(builder);
            HttpClient client = server.CreateClient();

            HttpContent data = new StringContent(JsonConvert.SerializeObject(stringToConvert), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/wordprocessing/" + convertMethodString, data);

            response.EnsureSuccessStatusCode();

            string res = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


    }
}
