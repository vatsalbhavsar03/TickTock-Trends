using System.Net.Mail;
using System.Net;
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
        //[HttpPost("CreatePayment")]
        //public async Task<IActionResult> CreatePayment([FromBody] PaymentDto dto)
        //{
        //    if (dto == null || dto.OrderId <= 0 || string.IsNullOrEmpty(dto.PaymentMethod)
        //        || string.IsNullOrEmpty(dto.TransactionId) || dto.Amount <= 0
        //        || string.IsNullOrEmpty(dto.PaymentStatus))
        //    {
        //        return BadRequest(new { success = false, message = "Invalid payment data." });
        //    }

        //    var order = await _context.Orders.FindAsync(dto.OrderId);
        //    if (order == null)
        //    {
        //        return NotFound(new { success = false, message = "Order not found." });
        //    }

        //    var payment = new Payment
        //    {
        //        OrderId = dto.OrderId,
        //        PaymentMethod = dto.PaymentMethod,
        //        TransactionId = dto.TransactionId,
        //        Amount = dto.Amount,
        //        PaymentStatus = dto.PaymentStatus,
        //        PaymentDate = DateTime.UtcNow,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };

        //    _context.Payments.Add(payment);
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        success = true,
        //        message = "Payment created successfully.",
        //        paymentId = payment.PaymentId
        //    });
        //}
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

            // ✅ Load full order with user and items
            var fullOrder = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == dto.OrderId);

            if (fullOrder != null)
            {
                await SendOrderConfirmationEmail(fullOrder.User.Email, fullOrder);
            }

            return Ok(new
            {
                success = true,
                message = "Payment created successfully and bill sent to email.",
                paymentId = payment.PaymentId
            });
        }

        private async Task<bool> SendOrderConfirmationEmail(string email, Order order)
        {
            try
            {
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "OrderConfirmation.html");
                string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);

                emailBody = emailBody.Replace("{{UserName}}", order.User.Name)
                                     .Replace("{{OrderId}}", order.OrderId.ToString())
                                     .Replace("{{OrderDate}}", order.CreatedAt.ToString("dd-MM-yyyy"))
                                     .Replace("{{TotalAmount}}", order.TotalAmount.ToString("C"));

                string itemsHtml = "";
                foreach (var item in order.OrderItems)
                {
                    itemsHtml += $"<tr>" +
                                 $"<td>{item.Product.Name}</td>" +
                                 $"<td>{item.Quantity}</td>" +
                                 $"<td>{item.Price}</td>" +
                                 $"</tr>";
                }
                emailBody = emailBody.Replace("{{OrderItems}}", itemsHtml);

                using var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("bhavsarvatsal337@gmail.com", "tptg vvyt sgdv qgho"),
                    EnableSsl = true,
                };

                var mailMsg = new MailMessage
                {
                    From = new MailAddress("bhavsarvatsal337@gmail.com"),
                    Subject = $"Order Confirmation - #{order.OrderId}",
                    Body = emailBody,
                    IsBodyHtml = true,
                };

                mailMsg.To.Add(email);
                await smtp.SendMailAsync(mailMsg);
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
