using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Models.DbEntities;

namespace UserManagement.Extensions
{
    public static class EntityTypeBuilderExtension
    {
        public static void ConfigureChangeTracking<T>(this EntityTypeBuilder<T> builder) where T: ChangeTrackingBase
        {
            builder.Property(e => e.CreatedAt)
                .HasPrecision(3)
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ModifiedAt)
                .HasPrecision(3);

            builder.Property(e => e.CreatedAt)
                .HasMaxLength(255);

            builder.Property(e => e.CreatedBy)
                .HasMaxLength(255);
        }
    }
}
