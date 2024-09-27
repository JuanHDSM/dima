namespace Dima.Core.Models.Stocks
{
    public class Stock
    {
        public string Currency { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string LongName { get; set; } = string.Empty;
        public decimal RegularMarketChange { get; set; }
        public decimal RegularMarketChangePercent { get; set; }
        public DateTime RegularMarketTime { get; set; }
        public decimal RegularMarketPrice { get; set; }
        public decimal RegularMarketDayHigh { get; set; }
        public string RegularMarketDayRange { get; set; } = string.Empty;
        public decimal RegularMarketDayLow { get; set; }
        public long RegularMarketVolume { get; set; }
        public decimal RegularMarketPreviousClose { get; set; }
        public decimal RegularMarketOpen { get; set; }
        public string FiftyTwoWeekRange { get; set; } = string.Empty;
        public decimal FiftyTwoWeekLow { get; set; }
        public decimal FiftyTwoWeekHigh { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal? PriceEarnings { get; set; }
        public decimal? EarningsPerShare { get; set; }
        public string LogoUrl { get; set; } = string.Empty;
    }
}