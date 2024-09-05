using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<long>> builder)
        {
            builder.ToTable("identity_user_token");
            builder.HasKey(ut => new {ut.UserId, ut.LoginProvider, ut.Name}).HasName("id");
            builder.Property(ut => ut.LoginProvider).HasColumnName("loginProvider").HasMaxLength(120);
            builder.Property(ut => ut.Name).HasColumnName("name").HasMaxLength(180);
            builder.Property(ut => ut.UserId).HasColumnName("userId");
            builder.Property(ut => ut.Value).HasColumnName("value");
        }
    }
}