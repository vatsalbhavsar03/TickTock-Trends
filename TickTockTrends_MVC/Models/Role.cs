namespace TickTockTrends_MVC.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>(); // One Role → Many Users
    }
}
