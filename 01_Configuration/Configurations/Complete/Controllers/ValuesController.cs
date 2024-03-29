﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Complete.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IOptionsSnapshot<TableOptions> configuration;

        public ValuesController(IOptionsSnapshot<TableOptions> config)
        {

            configuration = config;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var pageSize = configuration.Value.PageSize;
            var sortDirection = configuration.Value.SortDirection;

            return new string[] { sortDirection, pageSize.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
