using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniDash.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class HomeController : Controller
    {
        // GET: api/Home
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }
    }
}
