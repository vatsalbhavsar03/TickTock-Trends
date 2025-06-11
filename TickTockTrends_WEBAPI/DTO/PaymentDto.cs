namespace TickTockTrends_WEBAPI.DTO
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string TransactionId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = null!;
    }
}
