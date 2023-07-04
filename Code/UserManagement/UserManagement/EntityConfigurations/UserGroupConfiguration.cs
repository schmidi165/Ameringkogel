using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Extensions;
using UserManagement.Models.DbEntities;

namespace UserManagement.EntityConfigurations
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable(nameof(UserGroup));

            builder.Property(e => e.Name)
                .HasMaxLength(255);

            builder.ConfigureChangeTracking();
        }
    }
}
