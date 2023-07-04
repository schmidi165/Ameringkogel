using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Extensions;
using UserManagement.Models.DbEntities;

namespace UserManagement.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            builder.Property(e => e.FirstName)
                .HasMaxLength(255);

            builder.Property(e => e.LastName)
                .HasMaxLength(255);

            builder.Property(e => e.EMail)
                .HasMaxLength(255);

            builder.HasOne(u => u.UserGroup)
                .WithMany(g => g.Users)
                .OnDelete(DeleteBehavior.SetNull);

            builder.ConfigureChangeTracking();
        }
    }
}
