using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;

namespace GymManagementSystem.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetAllPaymentListAsync();
        Task<IEnumerable<PaymentDTO>> GetPaymentByMemberAsync(int MemberId);
        Task<PaymentDTO> CreatePaymentAsync(PaymentModel payment);

    }
}
