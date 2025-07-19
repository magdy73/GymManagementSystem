using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly AppDbContext context;

        public TrainerService(AppDbContext context) 
        {
            this.context = context;
        }
        public async Task<TrainerModel> CreateTrainerAsync(TrainerModel trainer)
        {
 
            context.trainers.Add(trainer);
            await context.SaveChangesAsync();
            return trainer;
        }

        public async Task<IEnumerable<TrainerModel>> GetAllTrainersAsync()
        {
            var trainers=await context.trainers.ToListAsync();
            return trainers;
        }

        public Task<TrainerModel?> GetTrainerByIdAsync(int id)
        {
            var trainer=context.trainers.Include(t => t.Members).SingleOrDefaultAsync(x => x.Id == id);
            return trainer;
        }

        public async Task<bool> UpdateTrainerAsync(int id, TrainerModel trainer)
        {
            if (trainer.Id == id)
                context.Entry(trainer).State = EntityState.Modified;
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
        public async Task<bool> DeleteTrainerAsync(int id)
        {   
            var trainer=await context.trainers.SingleOrDefaultAsync( x => x.Id == id);
            if (trainer == null)return false;

            context.trainers.Remove(trainer);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
