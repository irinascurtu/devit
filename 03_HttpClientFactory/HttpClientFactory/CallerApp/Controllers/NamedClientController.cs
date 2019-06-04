using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CallerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamedClientController : ControllerBase
    {
        private IHttpClientFactory clientFactory;
        public NamedClientController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "repos/aspnet/docs/pulls");

            var client = clientFactory.CreateClient("github");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //bla bla
            }
            else
            {
                // log and bla bla
            }

            return "named client value";
        }
    }
}