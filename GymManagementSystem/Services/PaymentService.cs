using GymManagementSystem.Data;
using GymManagementSystem.DataModels;
using GymManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext context;

        public PaymentService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<PaymentDTO>> GetAllPaymentListAsync()
        {
            var payments = await context.payment.Include(p => p.Member).OrderByDescending(p => p.PaymentDate)
                .Select(p=>new PaymentDTO
                {
                    Id=p.Id,
                    MemberId=p.memberId,
                    MemberName=p.Member.Name,
                    PaymentDate= p.PaymentDate.ToString("yyyy-mm-dd hh:mm tt"),
                    PaymentMethod=p.PaymentMethod,
                    Notes=p.Notes
                  
                }).ToListAsync();
            return payments;
        }
        public async Task<IEnumerable<PaymentDTO>> GetPaymentByMemberAsync(int MemberId)
        {
            var Payment = await context.payment.Where(p => p.memberId == MemberId).Include(p => p.Member).OrderByDescending(p => p.PaymentDate)
                .Select(p => new PaymentDTO
                {
                    Id = p.Id,
                    MemberId=p.memberId,
                    MemberName=p.Member.Name,
                    PaymentDate= p.PaymentDate.ToString("yyyy-mm-dd hh:mm tt"),
                    PaymentMethod=p.PaymentMethod,
                    Notes=p.Notes
                }).ToListAsync();
            return Payment;

        }
        public async Task<PaymentDTO> CreatePaymentAsync(PaymentModel payment)
        {
            payment.PaymentDate = DateTime.Now;
            context.payment.Add(payment);
            await context.SaveChangesAsync();

            var member = await context.members.FindAsync(payment.memberId);

            var today = DateOnly.FromDateTime(DateTime.Today);
            if(member.MembershipEndDate<today)
            {
                member.MembershipStartDate = today;
                member.MembershipEndDate = today.AddMonths(payment.DurationByMonths);
            }
            else
                member.MembershipEndDate = member.MembershipEndDate.AddMonths(payment.DurationByMonths);

            await context.SaveChangesAsync();

            var CreatedPayment = new PaymentDTO
                {
                    Id = payment.Id,
                    MemberId = payment.memberId,
                    MemberName = member?.Name ?? "Unknown",
                    PaymentDate = payment.PaymentDate.ToString("yyyy-MM-dd hh:mm tt"),
                    EndDate = member.MembershipEndDate.ToString("yyyy-MM-dd"),
                    PaymentMethod = payment.PaymentMethod,
                    Notes = payment.Notes
                };
            return CreatedPayment;
        }

       

       
    }
}
