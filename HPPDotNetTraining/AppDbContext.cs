using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPPDotNetTraining
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = "2404NB600260\\SQLEXPRESS",
                    InitialCatalog = "HPPDotNetTraining",
                    UserID = "sa",
                    Password = "hpp",
                    TrustServerCertificate = true,

                };
                optionsBuilder.UseSqlServer(stringBuilder.ConnectionString);
            }
        }
        public DbSet<UserDto> Users { get; set; }
    }
    [Table("Tbl_User")]
    public class UserDto
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
