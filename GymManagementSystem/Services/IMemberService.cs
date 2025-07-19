using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using System.Threading.Tasks;

namespace GymManagementSystem.Services
{
    
    public interface IMemberService
    { 
        Task<IEnumerable<MemberModel>> GetAllMembersAsync();
        Task<MemberModel?> GetMemberByIdAsync(int id);
        Task<MemberModel> CreateMemberAsync(MemberModel member);
        Task<bool> UpdateMemberAsync(int id, MemberModel member);
        Task<bool> DeleteMemberAsync(int id);
        Task<IEnumerable<ExpiringMemberDTO>> GetExpiringMemberAsync(int daysAhead);
    }
}
