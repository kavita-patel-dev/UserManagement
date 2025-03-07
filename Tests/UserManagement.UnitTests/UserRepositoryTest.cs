using Microsoft.EntityFrameworkCore;
using Moq;
using UserManagement.Domain;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.DBContext;

namespace UserManagement.UnitTests
{
    public class UserRepositoryTest
    {
        private readonly Mock<DbSet<User>> _mockUserDbSet;
        private readonly Mock<DbContext> _mockDbContext;
        private readonly UserRepository _repository;

        private PostgreSQLContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<PostgreSQLContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Use in-memory DB
                .Options;

            return new PostgreSQLContext(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsNull_WhenUserNotFound()
        {
            var context = GetInMemoryDbContext();
            context.Users.AddRange(new List<User>
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Doe" }
            });
            await context.SaveChangesAsync();

            var repository = new UserRepository(context);


            // Act
            var (data, totalCount) = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(data);
            Assert.Equal(2, totalCount);
            Assert.Equal("John Doe", data.FirstOrDefault().Name);
            Assert.Equal("Jane Doe", data.LastOrDefault().Name);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new UserRepository(context);

            // Act
            var (data, totalCount) = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(data);
            Assert.Empty(data);
            Assert.Equal(0, totalCount);
        }
    }
}
