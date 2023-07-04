using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Extensions;
using UserManagement.Models.DbEntities;

namespace UserManagement.EntityConfigurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project));

            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.ConfigureChangeTracking();
        }
    }
}
