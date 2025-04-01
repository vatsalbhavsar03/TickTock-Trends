namespace TickTockTrends_WEBAPI.DTO
{
    public class RegisterUserDTO
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Password { get; set; } = null!;

        // Optional: Allow setting CreatedAt and UpdatedAt (for future flexibility)
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
