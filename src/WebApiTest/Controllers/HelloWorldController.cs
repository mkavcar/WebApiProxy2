using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HelloWorldController : Controller
    {
        [HttpGet]
        public HelloWorldModel Get(string world)
        {
            if (string.IsNullOrEmpty(world)) world = "world";
            return new HelloWorldModel { text = $"Hello {world}" };
        }
    }

    public class HelloWorldModel 
    {
        public string text { get; set; }
    }
}
