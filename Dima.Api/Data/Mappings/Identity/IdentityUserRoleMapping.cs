using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityUserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
        {
            builder.ToTable("identity_user_role");
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.Property(x => x.UserId).HasColumnName("userId");
            builder.Property(x => x.RoleId).HasColumnName("roleId");
        }
    }
}