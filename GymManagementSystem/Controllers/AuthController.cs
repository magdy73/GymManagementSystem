using GymManagementSystem.DTO;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            var token=authService.LoginAsync(loginDTO);
            if (token == null) 
                return Unauthorized("Invalid Username or Password"); 
            return Ok(new { token });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {
            var IsRegistered=await authService.Register(registerDTO);
            if (!IsRegistered)
                return BadRequest("User already exists");
            return Ok("User registered successfully");

        }
    }
}
