using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext context;

        public AdminService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<UserInfoDTO>> GetAllUsers()
        {
            return await context.user
                .Select(u=>new UserInfoDTO
                {
                    Id= u.Id,
                    Role = u.Role,
                    UserName = u.UserName,
                }).ToListAsync();
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user=await context.user.FindAsync(id);
            if (user == null || user.Role=="Admin")
                return false;
            context.Remove(user);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> AssignTrainer(int MemberId, int TrainerId)
        {
            var member =await context.members.FindAsync(MemberId);
            var trainer=await context.trainers.FindAsync(TrainerId);
            if (member == null || trainer == null) return false;
            member.TrainerId=TrainerId;
            await context.SaveChangesAsync();
            return true;
        }

      

       
    }
}
