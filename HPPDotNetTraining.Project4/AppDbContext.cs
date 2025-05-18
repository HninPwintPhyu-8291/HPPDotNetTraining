using HPPDotNetTraining.Project4;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Language> Languages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=2404NB600260\\SQLEXPRESS;Database=HPPDotNetTraining;User Id=sa;Password=hpp;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>().ToTable("Tbl_Language");
        modelBuilder.Entity<Language>().Property(l => l.LanguageName).HasColumnName("Language");
    }
}
