using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTestCore1.Interfaces;
using WebApiTestCore1.Models;
using System.Data.SqlClient;

namespace WebApiTestCore1.Services
{
    public class UserService : IUserService
    {
        private const string cachekey = "UserList";
        private IDbContext context;
        
        
        public UserService(IDbContext Context)
        {
            context = Context;
        }

        public IEnumerable<Users> GetAll()
        {
            return context.Users.FromSql<Users>("select a.ID, Name, Age from dbo.TableA a join dbo.TableB b on a.id = b.id").ToList();
        }
    }
}
