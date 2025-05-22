namespace TickTockTrends_WEBAPI.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual Brand Brand { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
    }
}
