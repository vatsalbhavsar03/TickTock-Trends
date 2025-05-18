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
    public class WishlistController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;
        public WishlistController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Wishlist
        [HttpPost("AddToWishlist")]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistDto Dto)
        {
            var existingWishlistItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == Dto.UserId && w.ProductId == Dto.ProductId);
            if (existingWishlistItem != null)
            {
                return Ok(new
                {
                    success = false,
                    message = "Product already in wishlist"
                });
            }
            var wishlistItem = new Wishlist
            {
                UserId = Dto.UserId,
                ProductId = Dto.ProductId,
                WishlistDate = DateTime.Now
            };
            _context.Wishlists.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Product added to wishlist",
                Review = wishlistItem
            });
        }

        // GET: api/Wishlist
        [HttpGet("GetWishlist/{userId}")]
        public async Task<IActionResult> GetWishlist(int userId)
        {
            var wishlistItems = await _context.Wishlists
                .Include(w => w.Product)
                .Where(w => w.UserId == userId)
                .ToListAsync();
            if (wishlistItems.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Wishlist is empty"
                });
            }
            return Ok(new
            {
                success = true,
                data = wishlistItems
            });
        }

        // DELETE: api/Wishlist
        [HttpDelete("RemoveFromWishlist")]
        public async Task<IActionResult> RemoveFromWishlist([FromBody] WishlistDto dto)
        {
            var wishlistItem = await _context.Wishlists
                .FirstOrDefaultAsync(w => w.UserId == dto.UserId && w.ProductId == dto.ProductId);
            if (wishlistItem == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Product not found in wishlist"
                });
            }
            _context.Wishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Product removed from wishlist"
            });
        }

    }
}
