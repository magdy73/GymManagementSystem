using GymManagementSystem.DataModels;

namespace GymManagementSystem.Services
{
    public interface ITrainerService
    {
        Task<IEnumerable<TrainerModel>> GetAllTrainersAsync();
        Task<TrainerModel?> GetTrainerByIdAsync(int id);
        Task<TrainerModel> CreateTrainerAsync(TrainerModel trainer);
        Task<bool> UpdateTrainerAsync(int id, TrainerModel trainer);
        Task<bool> DeleteTrainerAsync(int id);
    }
}
