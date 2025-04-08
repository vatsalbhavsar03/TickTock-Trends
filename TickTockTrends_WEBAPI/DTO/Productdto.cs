namespace TickTockTrends_WEBAPI.DTO
{
    public class Productdto
    {

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
