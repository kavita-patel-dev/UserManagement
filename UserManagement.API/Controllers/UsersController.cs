using Microsoft.AspNetCore.Mvc;
using UserManagement.Application;
using UserManagement.Application.Interfaces;

namespace UserManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) { _userService = userService; }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            if (take < 1 || take > 100)
            {
                return BadRequest("Take must be between 1 and 100");
            }
            var result = await _userService.GetUsersAsync(skip, take);
            if(result.IsSuccess)
            {
                if(result.Data == null ||  result.Data.Count == 0) 
                    return NotFound();
                else
                    return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

    }
}
