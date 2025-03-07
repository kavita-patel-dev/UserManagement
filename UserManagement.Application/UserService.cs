using Microsoft.Extensions.Logging;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Responses;
using UserManagement.Domain;
using UserManagement.Infrastructure.Interfaces;

namespace UserManagement.Application
{
    public class UserService : IUserService
    {
        //private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) { _userRepository = userRepository; }

        public async Task<ResultResponse<List<User>>> GetUsersAsync(int skip, int take)
        {
            var result = new ResultResponse<List<User>> { IsSuccess = true };

            try
            {
                var (users, totalUsers) = (await _userRepository.GetAllAsync(skip, take));
                result.Data = users.ToList();
                result.TotalCount = totalUsers;
            }
            catch (Exception ex)
            {
                //_logger?.LogError(ex, ex.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
