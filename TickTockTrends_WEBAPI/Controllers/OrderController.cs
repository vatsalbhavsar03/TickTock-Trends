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
    public class OrderController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;
        public OrderController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Payments)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var result = orders.Select(order => new
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                UserName = order.User?.Name,
                Email = order.User?.Email,
                Phone = order.Phone,
                Address = order.Address,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                TransactionId = order.Payments.FirstOrDefault()?.TransactionId,
                Items = order.OrderItems.Select(oi => new
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name,
                    ImageUrl = oi.Product?.ImageUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    Total = oi.Quantity * oi.Price
                })
            });

            return Ok(new
            {
                success = true,
                orders = result
            });
        }



        
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto Dto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == Dto.UserId);

            if (cart == null || cart.CartItems.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Cart is empty"
                });
            }

            var totalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);

            var order = new Order
            {
                UserId = Dto.UserId,
                TotalAmount = totalAmount,
                Status = Status.Pending.ToString(),
                OrderDate = DateTime.Now,
                Phone = Dto.Phone,
                Address = Dto.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in cart.CartItems)
            {
                var product = item.Product;

                
                if (product.Stock < item.Quantity)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Insufficient stock for product: {product.Name}"
                    });
                }

                product.Stock -= item.Quantity;
                if (product.Stock <= 5)
                {
                    Console.WriteLine($"⚠️ Low stock alert: {product.Name} has only {product.Stock} items left.");
                 
                }

                
                if (product.Stock < 0)
                    product.Stock = 0;

                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.CartItems.RemoveRange(cart.CartItems);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Order created successfully",
                orderId = order.OrderId
            });
        }


        // GET /api/orders?userId=1 - List user's orders
        [HttpGet("GetUserOrder")]
        public async Task<IActionResult> GetUserOrders([FromQuery] int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Orders fetched successfully",
                Orders = orders
            });
        }

        // GET /api/orders/{id} - Get order details
        //[HttpGet("OrderDetails/{Orderid}")]
        //public async Task<IActionResult> GetOrderDetails(int Orderid)
        //{
        //    var order = await _context.Orders
        //        .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(o => o.OrderId == Orderid);

        //    if (order == null)
        //    {
        //        return NotFound(new
        //        {
        //            success = false,
        //            message = "Order not found"
        //        });
        //    }

        //    return Ok(new
        //    {
        //        success = true,
        //        message = "Order details fetched successfully",
        //        Order = order
        //    });
        //}
        [HttpGet("OrderDetails/{Orderid}")]
        public async Task<IActionResult> GetOrderDetails(int Orderid)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.User)
                .Include(o => o.Payments) // ✅ Include Payment data
                .FirstOrDefaultAsync(o => o.OrderId == Orderid);

            if (order == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Order not found"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Order details fetched successfully",
                order = new
                {
                    order.OrderId,
                    order.OrderDate,
                    order.Status,
                    order.TotalAmount,
                    order.Address,
                    order.Phone,
                    order.UserId,
                    order.User.Name,
                    order.User.Email,

                    // ✅ Include payment data
                    Payments = order.Payments.Select(p => new
                    {
                        p.PaymentMethod,
                        p.TransactionId,
                        p.Amount,
                        p.PaymentStatus,
                        p.PaymentDate
                    }),

                    OrderItems = order.OrderItems.Select(oi => new
                    {
                        oi.Quantity,
                        Product = new
                        {
                            oi.Product.Name,
                            oi.Product.Price
                        }
                    })
                }
            });
        }


        // PUT /api/orders/{id}/cancel - Cancel order
        [HttpPut("orders/{Orderid}/cancel")]
        public async Task<IActionResult> CancelOrder(int Orderid)
        {
            var order = await _context.Orders.FindAsync(Orderid);
            if (order == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Order not found"
                });
            }

            if (order.Status != Status.Pending.ToString())
            {
                return BadRequest("Only pending orders can be cancelled.");
            }
                

            order.Status = Status.Cancelled.ToString();
            order.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok("Order cancelled successfully.");
        }

        // GET /api/admin/orders - List  orders by status
        [HttpGet("GetOrdersByStatus")]
        public async Task<IActionResult> GetOrdersByStatus([FromQuery] string? status)
        {
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }
                

            var orders = await query.ToListAsync();
            return Ok(new
            {
                success = true,
                message = "Orders fetched successfully",
                Orders = orders
            });
        }

        
        [HttpPut("UpdateOrderStatus/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] UpdateOrderStatusDto model)
        {
            var order = await _context.Orders
                .Include(o => o.Payments) 
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return NotFound(new { success = false, message = "Order not found" });

            order.Status = model.Status;
            order.UpdatedAt = DateTime.Now;

            var payment = order.Payments.FirstOrDefault();
            if (payment != null &&
                payment.PaymentMethod == "COD" &&
                model.Status == "Delivered" &&
                payment.PaymentStatus != "Paid")
            {
                payment.PaymentStatus = "Paid";
                payment.UpdatedAt = DateTime.Now;
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Order status updated successfully." });
        }




        // GET /api/admin/orders/stats - Get statistics
        [HttpGet("AllStatistics")]
        public async Task<IActionResult> AllStatistics()
        {
            var totalOrders = await _context.Orders.CountAsync();
            var totalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount);
            var statusGroups = await _context.Orders
                .GroupBy(o => o.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();
            var totalCustomers = await _context.Users.CountAsync(u => u.RoleId == 2);
            var totalProducts = await _context.Products.CountAsync();
            var totalCategories = await _context.Categories.CountAsync();
            var totalBrands = await _context.Brands.CountAsync();

            return Ok(new
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                OrdersByStatus = statusGroups,
                TotalCustomers = totalCustomers,
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalBrands = totalBrands
            });
        }

    }
}
