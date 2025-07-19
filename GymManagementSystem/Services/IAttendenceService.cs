using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;

namespace GymManagementSystem.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDTO>> GetAllAttendancesAsync();
        Task<IEnumerable<AttendanceDTO>> GetAttendancebyIDAsync(int memberId);
        Task<AttendanceDTO> CreateAttendanceAsync(int memberId);

        Task<IEnumerable<AttendanceDTO>> FilterAsync(AttendenceFilterDTO filter);

    }
}
