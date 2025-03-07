using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using UserManagement.Domain;

namespace UserManagement.IntegrationTest
{
    public class UserControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public UserControllerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetUsers_ReturnsOk_WhenUserExists()
        {
            // Act
            var response = await _client.GetAsync($"api/Users");

            // Assert
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync(); // Get JSON as string
            var user = JsonSerializer.Deserialize<User>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal("John Doe", user.Name);
        }

        [Fact]
        public async Task GetUsers_ReturnsNotFound_WhenUsersDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"api/Users");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}