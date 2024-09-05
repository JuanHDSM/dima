using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity
{
    public class IdentityUserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("identity_user");
            builder.HasKey(x => x.Id).HasName("id");

            builder.HasIndex(u => u.NormalizedUserName).IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).IsUnique();

            builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(180);
            builder.Property(u => u.NormalizedEmail).HasColumnName("normalizedEmail").HasMaxLength(180);
            builder.Property(u => u.UserName).HasColumnName("name").HasMaxLength(180);
            builder.Property(u => u.NormalizedUserName).HasColumnName("normalizedUserName").HasMaxLength(180);
            builder.Property(u => u.PhoneNumber).HasColumnName("phoneNumber").HasMaxLength(20);
            builder.Property(u => u.ConcurrencyStamp).HasColumnName("concurrencyStamp").IsConcurrencyToken();
            builder.Property(u => u.EmailConfirmed).HasColumnName("emailConfirmed");
            builder.Property(u => u.PasswordHash).HasColumnName("passwordHash");
            builder.Property(u => u.SecurityStamp).HasColumnName("securityStamp");
            builder.Property(u => u.PhoneNumberConfirmed).HasColumnName("phoneNumberConfirmed");
            builder.Property(u => u.TwoFactorEnabled).HasColumnName("twoFactorEnabled");
            builder.Property(u => u.LockoutEnd).HasColumnName("lockoutEnd");
            builder.Property(u => u.LockoutEnabled).HasColumnName("lockoutEnabled");
            builder.Property(u => u.AccessFailedCount).HasColumnName("accessFailedCount");


            builder.HasMany<IdentityUserClaim<long>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            builder.HasMany<IdentityUserLogin<long>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            builder.HasMany<IdentityUserToken<long>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            builder.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}