using GymManagementSystem.DTO;

namespace GymManagementSystem.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<UserInfoDTO>> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task<bool>AssignTrainer(int MemberId,int TrainerId);
    }
}
