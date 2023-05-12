using ExamManagement.API.Models;
using ExamManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public IDbService DbService { get; }
        public UserController(IDbService dbService)
        {
            DbService = dbService;
        }

        [HttpGet("User")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await DbService.GetUsersAsync();
            return Ok(users);
        }
    }
}
