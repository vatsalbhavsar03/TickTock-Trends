namespace TickTockTrends_WEBAPI.DTO
{
    public class AddCartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public int UserId { get; set; }
    }
}
