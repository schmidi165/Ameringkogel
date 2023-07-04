using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UserManagement.EntityConfigurations;
using UserManagement.Models.DbEntities;

namespace UserManagement.Data
{
    public sealed class MSDbContext: DbContext
    {
        public MSDbContext()
        {
            ChangeTracker.Tracked += HandleTracked;
            ChangeTracker.StateChanged += HandleChangeTracking;
        }

        public MSDbContext(DbContextOptions<MSDbContext> options): base(options)
        {
            ChangeTracker.Tracked += HandleTracked;
            ChangeTracker.StateChanged += HandleChangeTracking;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupConfiguration());
            modelBuilder.ApplyConfiguration(new WorkItemConfiguration());
        }

        private void HandleTracked(object? sender, EntityTrackedEventArgs e)
        {
            if (e.Entry.Entity is ChangeTrackingBase entity && e.Entry.State == EntityState.Added)
            {
                entity.CreatedBy = "mschmid";
                entity.CreatedAt = DateTime.UtcNow;
            }
        }

        private void HandleChangeTracking(object? sender, EntityStateChangedEventArgs e)
        {
            if(e.Entry.Entity is ChangeTrackingBase entity && e.Entry.State == EntityState.Modified)
            {
                    entity.ModifiedAt = DateTime.UtcNow;
                    entity.ModifiedBy = "mschmid";
            }
        }
    }
}
