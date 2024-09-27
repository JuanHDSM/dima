using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Dima.Core.Responses.Stocks;
using stocks.Responses;

namespace Dima.Core.Handlers
{
    public interface IStockHandler
    {
        Task<Response<StockResponse>> GetAllStocksAsync (GetAllStocksRequest request);
        Task<Response<StocksBySymbolResponse>> GetStocksBySymbolAsync(GetStockBySymbolRequest request);
    }
}