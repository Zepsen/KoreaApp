using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var str = @"Data Source=EPUAKYIW3729;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var str = @"Data Source=DESKTOP-3OG4GO9;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            optionsBuilder.UseSqlServer(str);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // использование Fluent API
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Good>()
                .HasOne(p => p.Category)
                .WithMany(t => t.Goods)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
