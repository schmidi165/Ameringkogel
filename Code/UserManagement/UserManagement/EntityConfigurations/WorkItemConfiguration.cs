using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Extensions;
using UserManagement.Models.DbEntities;

namespace UserManagement.EntityConfigurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable(nameof(WorkItem));

            builder.Property(e => e.DueDate).HasPrecision(3);
            builder.Property(e => e.MinStartDate).HasPrecision(3);
            builder.Property(e => e.EstimatedTimeEffort).HasPrecision(3);

            builder.HasOne(w => w.User)
                .WithMany(u => u.WorkItems)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ConfigureChangeTracking();
        }
    }
}
