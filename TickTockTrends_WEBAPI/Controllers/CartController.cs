using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TickTockTrends_WEBAPI.Data;
using TickTockTrends_WEBAPI.Models;
using TickTockTrends_WEBAPI.DTO;

namespace TickTockTrends_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;

        public CartController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET: api/Cart/GetCart
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart(int userId)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    return NotFound(new
                    {
                        success = true,
                        message = "Cart is empty",
                        items = new List<object>()
                    });
                }

                var cartItems = cart.CartItems.Select(ci => new
                {
                    ci.CartitemId,
                    ci.ProductId,
                    ProductName = ci.Product.Name,
                    ci.Product.Price,
                    ci.Quantity,
                    Subtotal = ci.Product.Price * ci.Quantity,
                    ImageUrl = ci.Product.ImageUrl,
                    Description = ci.Product.Description
                }).ToList();

                return Ok(new
                {
                    success = true,
                    cartId = cart.CartId,
                    items = cartItems
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        // POST: api/Cart/AddToCart
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemDto cartItemDto)
        {
            try
            {
                // Check if product exists
                var product = await _context.Products.FindAsync(cartItemDto.ProductId);
                if (product == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Product not found"
                    });
                }

                // Get or create cart
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .FirstOrDefaultAsync(c => c.UserId == cartItemDto.UserId)
                    ?? new Cart { UserId = cartItemDto.UserId };

                if (cart.CartId == 0) _context.Carts.Add(cart);

                // Update existing item or add new
                var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += cartItemDto.Quantity;
                }
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductId = cartItemDto.ProductId,
                        Quantity = cartItemDto.Quantity
                    });
                }

                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Item added to cart",
                    cartId = cart.CartId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // PUT: api/Cart/UpdateCart/5
        [HttpPut("{cartItemId}/UpdateCart")]
        public async Task<IActionResult> UpdateCartItem(int cartItemId, [FromBody] UpdateCartItemDto updateDto)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Cart item not found"
                    });
                }

                if (updateDto.Quantity <= 0)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Quantity must be greater than 0"
                    });
                }

                cartItem.Quantity = updateDto.Quantity;
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Cart item updated",
                    cart = cartItem
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/Cart/DeleteCartItem/5
        [HttpDelete("{cartItemId}/DeleteCart")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            try
            {
                var cartItem = await _context.CartItems.FindAsync(cartItemId);
                if (cartItem == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Cart item not found"
                    });
                }

                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Item removed from cart"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
