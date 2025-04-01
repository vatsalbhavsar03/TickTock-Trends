using TickTockTrends_WEBAPI.Models;

namespace TickTockTrends_WEBAPI.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>(); // One Role → Many Users
    }
}


