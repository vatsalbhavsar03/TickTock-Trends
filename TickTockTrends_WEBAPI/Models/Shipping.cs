namespace TickTockTrends_WEBAPI.Models
{
    public partial class Shipping
    {
        public int ShippingId { get; set; }

        public int OrderId { get; set; }

        public string ShippingAddress { get; set; } = null!;

        public DateTime? ShippedDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string ShippingStatus { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
    }

    public enum ShippingStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
}
