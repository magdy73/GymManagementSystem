using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Services
{
    public class MemberService : IMemberService
    {
        private readonly AppDbContext context;

        public MemberService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<MemberModel> CreateMemberAsync(MemberModel member)
        {
            context.members.Add(member);
            await context.SaveChangesAsync();
            return member;
        }

        public async Task<IEnumerable<MemberModel>> GetAllMembersAsync()
        {
            var members =await context.members.ToListAsync();
            return (members);
        }

        public async Task<MemberModel?> GetMemberByIdAsync(int id)
        {
            var member=await context.members.SingleOrDefaultAsync(x => x.Id == id);
            return member;
        }

        public async Task<bool> UpdateMemberAsync(int id, MemberModel member)
        {
            if (id == member.Id) 
                context.Entry(member).State = EntityState.Modified;
            else
                return false;
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member=await context.members.FindAsync(id);
            if (member == null) return false;

            context.members.Remove(member);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ExpiringMemberDTO>> GetExpiringMemberAsync(int daysAhead)
        {
            var today= DateOnly.FromDateTime(DateTime.Today);
            var limitDate=today.AddDays(daysAhead);
            var members = await context.members.Where(m => m.MembershipEndDate <= limitDate).ToListAsync();
            var result =  members.Select(m=>new ExpiringMemberDTO
            {
                Id=m.Id,
                Name=m.Name,
                MembershipEndDate=m.MembershipEndDate,
                Status= m.MembershipEndDate>today? "ExpiringSoon" :"Expired"
            }).ToList();
            return result;
        }
    }
}
