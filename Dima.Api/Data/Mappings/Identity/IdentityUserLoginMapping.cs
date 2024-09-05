using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<long>> builder)
        {
            builder.ToTable("identity_login");
            builder.HasKey(l => new {l.LoginProvider, l.ProviderKey}).HasName("id");
            builder.Property(l => l.LoginProvider).HasColumnName("loginProvider").HasMaxLength(128);
            builder.Property(l => l.ProviderKey).HasColumnName("providerKey").HasMaxLength(128);
            builder.Property(l => l.ProviderDisplayName).HasColumnName("providerDisplayName").HasMaxLength(255);
            builder.Property(l => l.UserId).HasColumnName("userId");
        }
    }
}