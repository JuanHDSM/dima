using stocks.Models;

namespace stocks.Responses
{
    public class StockResponse
    {
        public List<Index> Indexes { get; set; } = new();
        public List<StockData> Stocks { get; set; } = new();
        public List<string> AvailableSectors { get; set; } = new();
        public List<string> AvailableStockTypes { get; set; } = new();
    }
}