using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired(false)
                .HasColumnType("VARCHAR")
                .HasMaxLength(245);

            builder.Property(x => x.UserId)
                .HasColumnName("userId")
                .IsRequired()
                .HasMaxLength(160);
        }
    }
}