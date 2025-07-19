using GymManagementSystem.DataModels;
using GymManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService PaymentService) 
        {
            paymentService = PaymentService;
        }
        [HttpGet]
        public async Task <IActionResult> GetAllPayments()
        {
            var payments =await paymentService.GetAllPaymentListAsync();
            return Ok(payments);
        }
        [HttpGet("memberId")]
        public async Task <IActionResult> GetMemberPayment(int memberId)
        {
            var payment = await paymentService.GetPaymentByMemberAsync(memberId);
            return Ok(payment);
        }
        [HttpPost]
        public async Task <IActionResult>CreatePayment(PaymentModel payment)
        {
            var CreatedPayment =await paymentService.CreatePaymentAsync(payment);
            return Ok(CreatedPayment);
        }

    }
}
