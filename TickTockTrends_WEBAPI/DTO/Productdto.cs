using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TickTockTrends_WEBAPI.DTO
{
    public class Productdto
    {
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0.01, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        // OPTIONAL: Image file only for create or when changed
        public IFormFile? ImageUrl { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
