using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Dima.Core.Responses.Stocks;
using stocks.Responses;

namespace Dima.Core.Handlers
{
    public interface IStockHandler
    {
        Task<Response<Stock>> CreateStockAsync(CreateStockRequest request);
        Task<PagedResponse<List<Stock>>> GetAllStocksAsync(GetAllStocksRequest request);
        Task<Response<StockResponse>> GetAllStocksExternalAsync (GetAllStocksRequest request);
        Task<Response<StocksBySymbolResponse>> GetStocksBySymbolExternalAsync(GetStockBySymbolRequest request);
        Task<Response<List<AssetsInWallet>>> GetAssetsInWalletAsync(GetAssetsInWalletRequest request);
    }
}