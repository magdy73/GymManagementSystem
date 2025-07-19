using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("You are an admin");
        }
        [Authorize]
        [HttpGet("authenticated-user")]
        public IActionResult Protected()
        {
            return Ok("You are authenticated.");
        }
        
        [HttpGet("whoami")]
        [Authorize]
        public IActionResult WhoAmI()
        {
            var name = User.Identity?.Name;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            return Ok(new { name, role });
        }

    }
}
