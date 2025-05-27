using System.ComponentModel.DataAnnotations;

namespace TickTockTrends_WEBAPI.DTO
{
    public class UpdateProfileDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNo { get; set; }
    }
}
