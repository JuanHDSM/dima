using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityRoleMapping : IEntityTypeConfiguration<IdentityRole<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
        {
            builder.ToTable("identity_role");
            builder.HasKey(x => x.Id).HasName("id");
            builder.HasIndex(x => x.NormalizedName).IsUnique();
            builder.Property(x => x.ConcurrencyStamp).HasColumnName("concurrencyStamp").IsConcurrencyToken();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(256);
            builder.Property(x => x.NormalizedName).HasColumnName("normalizedName").HasMaxLength(256);

        }
    }
}