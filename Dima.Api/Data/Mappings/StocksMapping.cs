using Dima.Core.Models.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings
{
    public class StocksMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("stocks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(x => x.Currency)
                .HasColumnName("currency")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.ShortName)
                .HasColumnName("shortName")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LongName)
                .HasColumnName("longName")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.RegularMarketChange)
                .HasColumnName("regularMarketChange")
                .IsRequired();

            builder.Property(x => x.RegularMarketChangePercent)
                .HasColumnName("regularMarketChangePercent")
                .IsRequired();

            builder.Property(x => x.RegularMarketTime)
                .HasColumnName("regularMarketTime")
                .IsRequired();

            builder.Property(x => x.RegularMarketPrice)
                .HasColumnName("regularMarketPrice")
                .IsRequired();

            builder.Property(x => x.RegularMarketDayHigh)
                .HasColumnName("regularMarketDayHigh")
                .IsRequired();

            builder.Property(x => x.RegularMarketDayRange)
                .HasColumnName("regularMarketDayRange")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.RegularMarketDayLow)
                .HasColumnName("regularMarketDayLow")
                .IsRequired();

            builder.Property(x => x.RegularMarketVolume)
                .HasColumnName("regularMarketVolume")
                .IsRequired();

            builder.Property(x => x.RegularMarketPreviousClose)
                .HasColumnName("regularMarketPreviousClose")
                .IsRequired();

            builder.Property(x => x.RegularMarketOpen)
                .HasColumnName("regularMarketOpen")
                .IsRequired();

            builder.Property(x => x.FiftyTwoWeekRange)
                .HasColumnName("fiftyTwoWeekRange")
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.FiftyTwoWeekLow)
                .HasColumnName("fiftyTwoWeekLow")
                .IsRequired();

            builder.Property(x => x.FiftyTwoWeekHigh)
                .HasColumnName("fiftyTwoWeekHigh")
                .IsRequired();

            builder.Property(x => x.Symbol)
                .HasColumnName("symbol")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.PriceEarnings)
                .HasColumnName("priceEarnings")
                .IsRequired(false);

            builder.Property(x => x.EarningsPerShare)
                .HasColumnName("earningsPerShare")
                .IsRequired(false);

            builder.Property(x => x.LogoUrl)
                .HasColumnName("logoUrl")
                .IsRequired(false)
                .HasMaxLength(250);

            builder.Property(x => x.UserId)
                .HasColumnName("userId")
                .IsRequired()
                .HasMaxLength(160);

            builder.HasIndex(x => x.Symbol)
                .HasDatabaseName("IX_stocks_symbol");
        }
    }
}