namespace Finux.Core.Requests.Stocks
{
    public class GetStockBySymbolRequest : BaseRequest
    {
        public string Symbol { get; set; } = string.Empty;
    }
}