using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCore1.Models;

namespace WebApiTestCore1.Interfaces
{
    public interface IUserService
    {
        IEnumerable<Users> GetAll();
    }
}
