using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        //respond to application/json
        [HttpGet]
        [Produces("application/json")]
        public ActionResult<IEnumerable<string>> GetAnIList()
        {
            var result = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                result.Add($"get {i} i-s");
            }
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // GET api/values
        //respond to application/json
        [HttpGet]
        [Consumes("custom/json")]
        public ActionResult<IEnumerable<string>> GetAnZList()
        {
            var result = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                result.Add($"get {i} z-s for custom responses");
            }
            return result;
        }
    }
}
