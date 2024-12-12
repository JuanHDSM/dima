using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class OrderMapping : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        
        builder.Property(x => x.Number)
            .IsRequired()
            .HasColumnName("number")
            .HasColumnType("VARCHAR")
            .HasMaxLength(8);
        
        builder.Property(x => x.ExternalReference)
            .IsRequired(false)
            .HasColumnName("external_reference")
            .HasColumnType("VARCHAR")
            .HasMaxLength(60);
        
        builder.Property(x => x.Gateway)
            .IsRequired()
            .HasColumnName("gateway")
            .HasColumnType("SMALLINT");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired()
            .HasColumnName("updated_at")
            .HasColumnType("DATETIME");
        
        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnName("status")
            .HasColumnType("SMALLINT");
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("user_id")
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.HasOne(x => x.Product).WithMany();
        builder.HasOne(x => x.Voucher).WithMany();

        builder.HasIndex(x => x.Number).IsUnique();
        builder.HasIndex(x => x.ExternalReference).IsUnique();
    }
}