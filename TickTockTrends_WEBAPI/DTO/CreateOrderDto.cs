namespace TickTockTrends_WEBAPI.DTO
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public double Phone { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
