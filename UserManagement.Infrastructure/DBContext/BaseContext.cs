using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;
using UserManagement.Infrastructure.DataSeeder;

namespace UserManagement.Infrastructure.DBContext
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(50);
            });

            modelBuilder.ApplyConfiguration(new UserDataSeeder());
        }
    }
}
