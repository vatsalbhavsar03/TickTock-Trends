using Microsoft.EntityFrameworkCore;
using TickTockTrends_WEBAPI.Models;

namespace TickTockTrends_WEBAPI.Data
{
    public class TickTockDbContext : DbContext
    {
        public TickTockDbContext(DbContextOptions<TickTockDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");  // Auto-generate CreatedAt

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()")   // Default value
                .ValueGeneratedOnAddOrUpdate();       // Update on every modification

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;  // Set UpdatedAt on update
                }
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<User>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;  // Set UpdatedAt on update
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
