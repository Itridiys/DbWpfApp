using Microsoft.EntityFrameworkCore;

namespace DbWpfApp.Models.Data
{
    public class AplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public AplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbWpfApp;Trusted_Connection=True;");
        }
    }
}
