using Moq;
using UserManagement.Application.Interfaces;
using UserManagement.API.Controllers;
using UserManagement.Domain;
using UserManagement.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.UnitTests
{
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        [Fact]
        public async Task GetUsers_Returns_OkResult()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe" },
                new User { Id = 2, Name = "Jane Smith" }
            };
            var resultResponse = new ResultResponse<List<User>>() { IsSuccess = true, Data = users, TotalCount = 2};
            _mockUserService.Setup(service => service.GetUsersAsync(1,10)).ReturnsAsync(resultResponse);

            // Act
            var result = await _controller.GetUsers(1, 10);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUsers_Returns_NotFoundResult_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>();
            var resultResponse = new ResultResponse<List<User>>() { IsSuccess = true, Data = users, TotalCount = 0 };
            _mockUserService.Setup(service => service.GetUsersAsync(1,10)).ReturnsAsync(resultResponse);

            // Act
            var result = await _controller.GetUsers(1, 10);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task GetUsers_Returns_BadRequestResult_WhenServiceException()
        {
            // Arrange
            var resultResponse = new ResultResponse<List<User>>() { IsSuccess = false, Data = null };
            _mockUserService.Setup(service => service.GetUsersAsync(1,10)).ReturnsAsync(resultResponse);

            // Act
            var result = await _controller.GetUsers(1,10);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task GetUsers_Returns_BadRequestResult_WhenInvalidTake()
        {
            // Arrange
            var resultResponse = new ResultResponse<List<User>>() { IsSuccess = false, Data = null };
            _mockUserService.Setup(service => service.GetUsersAsync(1, 10)).ReturnsAsync(resultResponse);

            // Act
            var result = await _controller.GetUsers(1, 0);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Take must be between 1 and 100", actionResult.Value);
        }
    }
}
