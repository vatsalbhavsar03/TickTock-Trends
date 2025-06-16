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
    public class ReviewController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;
        public ReviewController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //GET: api/Review
        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .OrderByDescending(r => r.ReviewDate)
                .Select(r => new
                {
                    r.ReviewId,
                    r.ProductId,
                    ProductName = r.Product.Name,
                    r.UserId,
                    UserName = r.User.Name,
                    r.Rating,
                    r.Comment,
                    r.ReviewDate
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                data = reviews
            });
        }


        // POST: api/Review
        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview([FromBody] ReviewDto Dto)
        {
            var review = new Review
            {
                UserId = Dto.UserId,
                ProductId = Dto.ProductId,
                Rating = Dto.Rating,
                Comment = Dto.Comment,
                ReviewDate = DateTime.Now
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Review added successfully",
                Review = review
            });
        }

        //PUT: api/Review
        [HttpPut("UpdateReview/{Reviewid}")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewDto Dto,int Reviewid)
        {
            var review = await _context.Reviews.FindAsync(Reviewid);
            if (review == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Review not found"
                });
            }
            review.Rating = Dto.Rating;
            review.Comment = Dto.Comment;
            review.ReviewDate = DateTime.Now;
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Review updated successfully",
                Review = review
            });
        }

        // GET: api/Review
        [HttpGet("GetReviews/{productId}")]
        public async Task<IActionResult> GetReviews(int productId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.ProductId == productId)
                .ToListAsync();
            if (reviews.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No reviews found"
                });
            }
            return Ok(new
            {
                success = true,
                message = "Reviews fetched successfully",
                data = reviews

            });
        }

        // GET: api/Review
        [HttpGet("GetUserReviews/{userId}")]
        public async Task<IActionResult> GetUserReviews(int userId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.Product)
                .Where(r => r.UserId == userId)
                .ToListAsync();
            if (reviews.Count == 0)
            {
                return NotFound(new
                {
                    success = false,
                    message = "No reviews found"
                });
            }
            return Ok(new
            {
                success = true,
                message = "Reviews fetched successfully",
                data = reviews
            });
        }

        // DELETE: api/Review
        [HttpDelete("DeleteReview/{Reviewid}")]
        public async Task<IActionResult> DeleteReview(int Reviewid)
        {
            var review = await _context.Reviews.FindAsync(Reviewid);
            if (review == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Review not found"
                });
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Review deleted successfully"
            });
        }
    }
}
