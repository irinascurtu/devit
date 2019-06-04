using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CallerApp
{
    public class MyAwesomeService
    {
        public HttpClient Client { get; }

        public MyAwesomeService(HttpClient client)
        {
            //or you can read the base address
            client.BaseAddress = new Uri("https://localhost:44381/");

            //add required headers
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",  "HttpClientFactory-Sample");
            client.DefaultRequestHeaders.Add("culture", "en-US");
            //assigne it
            Client = client;
        }

        public async Task<IEnumerable<string>> GetEverythingWithTheCulture()
        {
            var response = await Client.GetAsync("api/values");

            response.EnsureSuccessStatusCode();
            //do sth
            var result = await response.Content
                .ReadAsAsync<IEnumerable<string>>();

            return result;
        }
    }
}