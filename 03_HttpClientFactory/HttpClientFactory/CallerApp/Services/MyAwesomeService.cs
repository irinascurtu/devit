﻿using System;
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
            client.BaseAddress = new Uri("http://localhost:5000/");

            //add required headers
            // GitHub API versioning
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("Content-Type",  "custom/json");
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