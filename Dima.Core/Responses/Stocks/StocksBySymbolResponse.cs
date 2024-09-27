
using Dima.Core.Models.Stocks;

namespace Dima.Core.Responses.Stocks
{
    public class StocksBySymbolResponse
    {
        public List<Stock> Results { get; set; } = new();
        public DateTime RequestedAt { get; set; }
        public string Took { get; set; } = string.Empty;
    }
}