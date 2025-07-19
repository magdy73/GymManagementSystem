using GymManagementSystem.DataModels;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) 
        {
        }
        public DbSet<MemberModel> members { get; set; }
        public DbSet<TrainerModel> trainers { get; set; }
        public DbSet<AttendanceModel> attendances { get; set; }
        public DbSet<PaymentModel> payment { get; set; }
        public DbSet<UserModel> user { get; set; }
    }
}
