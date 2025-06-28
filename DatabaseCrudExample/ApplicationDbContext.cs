using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCrudExample
{
        public class ApplicationDbContext : DbContext
        {
            public DbSet<Product> Products { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            optionsBuilder.UseSqlServer("Server=2404NB600260\\SQLEXPRESS;Database=TestDb;User Id=sa;Password=hpp;TrustServerCertificate=True;");
        }
        }
    }
