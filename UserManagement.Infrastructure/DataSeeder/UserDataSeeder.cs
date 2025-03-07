using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain;

namespace UserManagement.Infrastructure.DataSeeder
{
    public class UserDataSeeder : IEntityTypeConfiguration<User>
    {
        public static List<User> Types => GenerateData();

        private static List<User> GenerateData()
        {
            var types = new List<User>
            {
                new User{Id = 1, Name = "John Doe"},
                new User{Id = 2, Name = "Jane Smith"},
                new User{Id = 3, Name = "Bob Marely"}
            };

            return types;
        }
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasData(GenerateData());
        }
    }
}
