using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTestCore1.Models;

namespace WebApiTestCore1.Services
{
    public partial class IDbContext : DbContext
    {
        public IDbContext(DbContextOptions<IDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Users> Users { get; set; }
    }
}
