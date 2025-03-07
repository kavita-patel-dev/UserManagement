using UserManagement.Application.Responses;
using UserManagement.Domain;

namespace UserManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<ResultResponse<List<User>>> GetUsersAsync(int skip, int take);
    }
}
