using Microsoft.EntityFrameworkCore;
using SinusSkateboards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinusSkateboards.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
