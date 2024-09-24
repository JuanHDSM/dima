namespace stocks.Models
{
    public class StockData
    {
        public string Stock { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Close { get; set; }
        public decimal? Change { get; set; } = null!;
        public decimal? Volume { get; set; }
        public long? MarketCap { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string? Sector { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}