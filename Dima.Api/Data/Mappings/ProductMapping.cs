using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("title")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnName("description")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnName("price");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnType("TINYINT");
        
        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.Title).IsUnique();
    }
}