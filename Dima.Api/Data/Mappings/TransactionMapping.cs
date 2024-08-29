using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {

            builder.ToTable("transaction");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("title")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.CreateAt)
                .IsRequired()
                .HasColumnName("createAt")
                .HasColumnType("DATETIME");

            builder.Property(x => x.PaidOrReceivedAt)
                .IsRequired(false)
                .HasColumnName("paidOrReceivedAt")
                .HasColumnType("DATETIME");

            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnName("type")
                .HasColumnType("TINYINT");

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("amount")
                .HasColumnType("DECIMAL");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("userId")
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);

            builder.Property(x => x.CategoryId)
                .HasColumnName("categoryId");
        }
    }
}