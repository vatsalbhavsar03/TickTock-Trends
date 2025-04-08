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
    }
}
