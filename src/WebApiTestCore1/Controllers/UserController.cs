using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiTestCore1.Interfaces;
using WebApiTestCore1.Models;
using Microsoft.Extensions.Logging;

namespace WebApiTestCore1.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;
        private ILogger<UserController> logger;

        public UserController(ILogger<UserController> Logger, IUserService UserService)
        {
            logger = Logger;
            userService = UserService;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<Users> GetAll()
        {
            logger.LogInformation("Get User List");
            return userService.GetAll();
        }
    }
}
