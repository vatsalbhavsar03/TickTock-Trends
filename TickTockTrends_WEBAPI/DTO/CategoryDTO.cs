using System.ComponentModel.DataAnnotations;

namespace TickTockTrends_WEBAPI.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; } = null!;
    }
}
