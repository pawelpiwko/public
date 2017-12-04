using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WordProcessingAPI.UnitTests.Controllers
{
    public class WordProcessingControllerTests
    {
        [Fact]
        public async Task WordProcrssingControllerBasicTest()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
                .UseApplicationInsights();
            
            TestServer server = new TestServer(builder);

            HttpClient client = server.CreateClient();

            string testStringInput = "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.";
            //JsonConvert.SerializeObject(< your object >)
            //var data = new Dictionary<string, string>() { };

            //var secretContent = new FormUrlEncodedContent(data);
            //var response = await client.PostAsync("/api/wordprocessing/csv", new StringContent(testStringInput, Encoding.UTF8, "application/json"));
            var response = await client.PostAsJsonAsync("/api/wordprocessing/csv", testStringInput);

            response.EnsureSuccessStatusCode();

            string res = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);

        }

    }
}
