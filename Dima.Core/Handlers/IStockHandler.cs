using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using stocks.Responses;

namespace Dima.Core.Handlers
{
    public interface IStockHandler
    {
        Task<Response<StockResponse>> GetAllStocksAsync (GetAllStocksRequest request);
    }
}