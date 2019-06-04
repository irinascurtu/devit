using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CallerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypedClientController : ControllerBase
    {
        private MyAwesomeService awesomeClient;

        public TypedClientController(MyAwesomeService client)
        {
            this.awesomeClient = client;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            return await awesomeClient.GetEverythingWithTheCulture();
        }
    }
}