using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application;
using UserManagement.Application.Responses;
using UserManagement.Domain;
using UserManagement.Infrastructure.Interfaces;

namespace UserManagement.UnitTests
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsListOfUsers_WhenUsersExist()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Doe" }
            };
            var totalUsers = 2;
            var returnResult = (users,totalUsers);

            _mockUserRepository.Setup(repo => repo.GetAllAsync(1, 10)).ReturnsAsync(returnResult);

            // Act
            var result = await _userService.GetUsersAsync(1,10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalCount);
            Assert.Equal("John Doe", result.Data[0].Name);
            Assert.Equal("Jane Doe", result.Data[1].Name);
        }

        [Fact]
        public async Task GetUsersAsync_ReturnsEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>();
            var totalUsers = 0;
            var returnResult = (users, totalUsers);
            _mockUserRepository.Setup(repo => repo.GetAllAsync(1, 10)).ReturnsAsync(returnResult);

            // Act
            var result = await _userService.GetUsersAsync(1,10);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Data);
            Assert.Equal(0, result.TotalCount);
        }
    }
}