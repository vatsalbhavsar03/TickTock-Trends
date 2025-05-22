namespace TickTockTrends_WEBAPI.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime WishlistDate { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
