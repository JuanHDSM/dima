namespace Dima.Core
{
    public static class Configuration
    {
        public const int DefaultSatusCode = 200;
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;
        public static string ConnectionString { get; set; } = string.Empty;
        public static string FrontendUrl { get; set; } = string.Empty;
        public static string BackendUrl { get; set; } = string.Empty;
        public static string StockApiUrl { get; set; } = string.Empty;
        public static string StocksHttpClientName { get; set; } = "stocks";
        public static string TokenStockService { get; set; } = "94op5kYVVLwmMPt1qZMTd8";
    }
}