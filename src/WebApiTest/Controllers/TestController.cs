using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello World" };
        }
    }
}
