namespace TickTockTrends_WEBAPI.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public double Phone { get; set; }

        public string Address { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();

        public virtual User User { get; set; } = null!;
    }
}
