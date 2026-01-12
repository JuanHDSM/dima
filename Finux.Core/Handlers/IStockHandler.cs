using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Stocks;
using Finux.Core.Responses;
using Finux.Core.Responses.Stocks;
using stocks.Responses;

namespace Finux.Core.Handlers
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