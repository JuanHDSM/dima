using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
        {
            builder.ToTable("identity_role_claim");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.ClaimType).HasColumnName("claimType").HasMaxLength(255);
            builder.Property(x => x.ClaimValue).HasColumnName("claimValue").HasMaxLength(255);
            builder.Property(x => x.RoleId).HasColumnName("roleId");
        }
    }
}