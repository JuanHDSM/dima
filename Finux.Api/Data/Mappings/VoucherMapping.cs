using Finux.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finux.Api.Data.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("voucher");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        
        builder.Property(x => x.Number).HasColumnName("number")
            .IsRequired()
            .HasColumnType("CHAR")
            .HasMaxLength(8);
        
        builder.Property(x => x.Title).HasColumnName("title")
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Description).HasColumnName("description")
            .IsRequired(false)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.IsActive).HasColumnName("is_active")
            .IsRequired()
            .HasColumnType("TINYINT");
        
        builder.HasIndex(x => x.Number).IsUnique();
        builder.HasIndex(x => x.Title).IsUnique();
    }
}