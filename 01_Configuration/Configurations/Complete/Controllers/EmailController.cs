using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Complete.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Complete.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private EmailConfiguration config;
        public EmailController(EmailConfiguration config)
        {
            this.config = config;
        }       
    }
}