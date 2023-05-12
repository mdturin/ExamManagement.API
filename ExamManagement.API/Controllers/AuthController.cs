using ExamManagement.API.Models;
using ExamManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService AuthService { get; }
        public UtilityService Service { get; }
        public IDbService DbService { get; }

        public AuthController(
            IAuthService authService, 
            UtilityService service, IDbService dbService)
        {
            Service = service;
            DbService = dbService;
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

            AuthService.CreatePasswordHash(
                request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = await DbService
                .CreateUserAsync(request, passwordHash, passwordSalt);
            
            return Ok(user);
        }
    }
}
