using Microsoft.EntityFrameworkCore;

namespace WEB_EXE201.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
                 : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.User) // Sử dụng HasOne thay vì HasRequired
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserID);
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        UserID = 1,
                        Username = "john_doe",
                        Password = "password123",
                        Email = "john.doe@example.com"
                    },
                    new User
                    {
                        UserID = 2,
                        Username = "jane_smith",
                        Password = "jane2023",
                        Email = "jane.smith@example.com"
                    });
            base.OnModelCreating(modelBuilder);
        }
    }
}
