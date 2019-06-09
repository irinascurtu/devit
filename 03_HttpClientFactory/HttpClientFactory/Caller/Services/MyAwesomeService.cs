using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallerApp
{
    public class MyAwesomeService
    {
        public HttpClient Client { get; }

        public MyAwesomeService(HttpClient client)
        {
            //or you can read the base address
            client.BaseAddress = new Uri("http://localhost:5000/api/values");

            //add required headers            
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("culture", "en-US");

            //assigne it
            Client = client;
        }

        public async Task<IEnumerable<string>> GetEverythingWithTheCulture()
        {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Client.BaseAddress);

            request.Content = new StringContent("",Encoding.UTF8, "custom/json");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("custom/json");
            var response = await Client.SendAsync(request);
               

            //explain why the first option
            //var response2 = await Client.GetAsync("api/values");
            
            response.EnsureSuccessStatusCode();
            //do sth
            var result = await response.Content
                .ReadAsAsync<IEnumerable<string>>();

            return result;
        }
    }
}