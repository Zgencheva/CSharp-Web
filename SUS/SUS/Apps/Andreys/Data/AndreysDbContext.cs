using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andreys.Data
{
    public class AndreysDbContext : DbContext
    {
        public AndreysDbContext()
        {
                
        }
        public AndreysDbContext(DbContext db)
        : base()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Andreys;Integrated Security= true;");
            }
        }
    }
}
