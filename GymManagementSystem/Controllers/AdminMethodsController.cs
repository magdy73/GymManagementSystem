using GymManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMethodsController : ControllerBase
    {
        private readonly IAdminService adminService;

        public AdminMethodsController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        [HttpGet("all")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await adminService.GetAllUsers();
            return Ok(users);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) 
        {
            var IsDeleted=await adminService.DeleteUser(id);
            if (IsDeleted)
                return Ok("User has been Deleted.");
            return BadRequest();
        }


        [HttpPut("Assign-Trainer")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AssginTrainer(int MemberId,int TrainerId)
        {
            var IsAssigned= await adminService.AssignTrainer(MemberId, TrainerId);
            if (IsAssigned)
                return Ok("Trainer has been Assigned to the user.");
            return BadRequest();
        }
    }
}
