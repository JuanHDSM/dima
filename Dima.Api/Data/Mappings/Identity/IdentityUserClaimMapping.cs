using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityUserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<long>> builder)
        {
            builder.ToTable("identity_claim");
            builder.HasKey(uc => uc.Id).HasName("id");
            builder.Property(uc => uc.ClaimType).HasColumnName("claimType").HasMaxLength(255);
            builder.Property(uc => uc.ClaimValue).HasColumnName("claimValue").HasMaxLength(255);
            builder.Property(uc => uc.UserId).HasColumnName("userId");
        }
    }
}