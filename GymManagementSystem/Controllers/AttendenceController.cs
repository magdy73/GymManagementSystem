using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendanceService attendenceService;

        public AttendenceController(IAttendanceService attendenceService)
        {
            this.attendenceService = attendenceService;

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        { 
            var att= await attendenceService.GetAllAttendancesAsync();
            return Ok(att);
        }
        [HttpGet("{MemberId}")]
        public async Task<IActionResult>GetById(int MemberId)
        {
            var att=await attendenceService.GetAttendancebyIDAsync(MemberId);
            return Ok(att);
        }
        [HttpPost("{MemberId}")]
        public async Task<IActionResult>AddAttendance(int MemberId)
        {
            var AddAtt=await attendenceService.CreateAttendanceAsync(MemberId);
            return Ok(AddAtt);
        }
        [HttpPost("filter")]
        public async Task<IActionResult> Filter([FromBody]AttendenceFilterDTO filter)
        {
            var result=await attendenceService.FilterAsync(filter);
            return Ok(result);
        }
    }
}
