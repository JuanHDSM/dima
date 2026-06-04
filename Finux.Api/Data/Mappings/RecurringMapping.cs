using Finux.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finux.Api.Data.Mappings;

public class RecurringMapping : IEntityTypeConfiguration<Recurring>
{
    public void Configure(EntityTypeBuilder<Recurring> builder)
    {
        builder.ToTable("recurring");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        
        builder.Property(x => x.Installments)
            .HasColumnName("installments")
            .HasColumnType("TINYINT");
            
        builder.Property(x => x.InstallmentsType)
            .HasColumnName("installmentsType")
            .HasColumnType("TINYINT");
            
        builder.Property(x => x.RecurringType)
            .HasColumnName("recurringType")
            .HasColumnType("TINYINT");
    }
}