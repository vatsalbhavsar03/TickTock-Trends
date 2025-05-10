using System.ComponentModel.DataAnnotations;

namespace TickTockTrends_WEBAPI.DTO
{
    public class BrandDTO
    {
        [Required(ErrorMessage = "Category ID is required.")]
        public int CategoryId { get; set; }

        public int BrandId { get; set; } 

        [Required(ErrorMessage = "Brand name is required.")]
        public string BrandName { get; set; } = null!;
    }
}
