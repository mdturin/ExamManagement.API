using ExamManagement.API.Models;
using ExamManagement.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        public IAuthService AuthService { get; }
        public UtilityService Service { get; }

        public AuthController(IAuthService authService, UtilityService service)
        {
            Service = service;
            AuthService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client request");
            }

            if (Service.ValidRegisterRequest(request) == false)
            {
                return BadRequest("Invalid client request");
            }

            var password = request.Password;
            AuthService.CreatePasswordHash(
                password, out byte[] passwordHash, out byte[] passwordSalt);
            user = AuthService.CreateUser(request, passwordHash, passwordSalt);
            return Ok(user);
        }
    }
}
