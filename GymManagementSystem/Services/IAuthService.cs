using GymManagementSystem.DTO;

namespace GymManagementSystem.Services
{
    public interface IAuthService
    {
        Task <string> LoginAsync(LoginDTO loginDTO);
    }
}
