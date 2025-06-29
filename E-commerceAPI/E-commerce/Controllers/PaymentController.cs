using E_commerce.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models.DTOs;

namespace E_commerce.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequestDTO request)
        {
            try
            {
                // Process the payment using the service
                var payment = await _paymentService.ProcessPaymentAsync(request.UserId, request.OrderId, request.Amount, request.PaymentMethod);

                // Return the payment DTO instead of the whole model
                var paymentDto = new PaymentDTO
                {
                    PaymentId = payment.PaymentId,
                    OrderId = payment.OrderId,
                    UserId = request.UserId,
                    Amount = payment.Amount,
                    PaymentMethod = payment.PaymentMethod,
                    PaymentStatus = payment.PaymentStatus, // Assuming status is set during payment processing
                    PaymentDate = payment.PaymentDate
                };

                return Ok(paymentDto); // Return Payment DTO instead of the full Payment object
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPaymentsByUserId(int userId)
        {
            var payments = await _paymentService.GetPaymentsByUserIdAsync(userId);
            return Ok(payments);
        }
    }

}
