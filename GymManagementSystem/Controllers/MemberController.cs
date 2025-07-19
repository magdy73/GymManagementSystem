using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }


        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            var members = await memberService.GetAllMembersAsync();
            return Ok(members);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id) {
            MemberModel member = await memberService.GetMemberByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
            
        }
        [HttpPost]
        public async Task<IActionResult> AddMember(MemberModel member )
        {
            MemberModel CreatedMember= await memberService.CreateMemberAsync(member);
            if (CreatedMember == null) return NotFound();
            return Ok(CreatedMember);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateMember(int id, MemberModel member)
        {
            var IsUpdeted=await memberService.UpdateMemberAsync(id, member);
            return IsUpdeted ? NoContent() : BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteMember(int id)
        {
            var IsDeleted = await memberService.DeleteMemberAsync(id);
            return IsDeleted ? NoContent() : BadRequest();
        }
        [HttpGet("expiring")]
        public async Task<ActionResult<List<ExpiringMemberDTO>>> GetExpiringMembers([FromQuery] int days = 7)
        {
            var result = await memberService.GetExpiringMemberAsync(days);
            return Ok(result);
        }

    }
}
