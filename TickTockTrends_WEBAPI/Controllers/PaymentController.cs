using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TickTockTrends_WEBAPI.Data;
using TickTockTrends_WEBAPI.DTO;
using TickTockTrends_WEBAPI.Models;

namespace TickTockTrends_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;

        public PaymentController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/GetAllPayments
        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _context.Payments
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            var result = payments.Select(payment => new PaymentDto
            {
                OrderId = payment.OrderId,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus
            });

            return Ok(new
            {
                success = true,
                message = "Payments fetched successfully.",
                payments = result
            });
        }


        // GET: api/Payment/GetPaymentById/{Orderid}
        [HttpGet("GetPaymentById/{Orderid}")]
        public async Task<IActionResult> GetPaymentById(int Orderid)
        {
            var payment = await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.OrderId == Orderid);
            if (payment == null)
            {
                return NotFound(new { success = false, message = "Payment not found." });
            }
            var result = new
            {
                PaymentId = payment.PaymentId,
                OrderId = payment.OrderId,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate,
                CreatedAt = payment.CreatedAt,
                UpdatedAt = payment.UpdatedAt
            };
            return Ok(new
            {
                success = true,
                payment = result
            });
        }

        // POST: api/Payment/CreatePayment
        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto dto)
        {
            if (dto == null || dto.OrderId <= 0 || string.IsNullOrEmpty(dto.PaymentMethod)
                || string.IsNullOrEmpty(dto.TransactionId) || dto.Amount <= 0
                || string.IsNullOrEmpty(dto.PaymentStatus))
            {
                return BadRequest(new { success = false, message = "Invalid payment data." });
            }

            var order = await _context.Orders.FindAsync(dto.OrderId);
            if (order == null)
            {
                return NotFound(new { success = false, message = "Order not found." });
            }

            var payment = new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                TransactionId = dto.TransactionId,
                Amount = dto.Amount,
                PaymentStatus = dto.PaymentStatus,
                PaymentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Payment created successfully.",
                paymentId = payment.PaymentId
            });
        }

    }
}
