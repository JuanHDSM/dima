using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Dima.Core.Responses.Stocks;
using stocks.Responses;

namespace Dima.Web.Handlers
{
    public class StockHandler(IHttpClientFactory httpClientFactory) : IStockHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Stock>> CreateStockAsync(CreateStockRequest request)
        {
            var result = await _client.PostAsJsonAsync("api/v1/stocks/quote", request);
            return await result.Content.ReadFromJsonAsync<Response<Stock>>()
                ?? new Response<Stock>(null, 400, "Falha ao adicionar ativo.");
        }

        public async Task<Response<StockResponse>> GetAllStocksAsync(GetAllStocksRequest request)
            => await _client.GetFromJsonAsync<Response<StockResponse>>("api/v1/stocks/quote/list")
                ?? new Response<StockResponse>(null, 400, "Falha ao obter ações");

        public async Task<Response<StockResponse>> GetAllStocksExternalAsync(GetAllStocksRequest request)
            => await _client.GetFromJsonAsync<Response<StockResponse>>("api/v1/stocks/quote/list")
                ?? new Response<StockResponse>(null, 400, "Falha ao obter ações");

        public async Task<Response<List<AssetsInWallet>>> GetAssetsInWalletAsync(GetAssetsInWalletRequest request)
            => await _client.GetFromJsonAsync<Response<List<AssetsInWallet>>>("api/v1/stocks/wallet")
                ?? new Response<List<AssetsInWallet>>(null, 400, "Falha ao obter ativos na carteira");

        public async Task<Response<StocksBySymbolResponse>> GetStocksBySymbolExternalAsync(GetStockBySymbolRequest request)
            => await _client.GetFromJsonAsync<Response<StocksBySymbolResponse>>($"api/v1/stocks/quote/{request.Symbol}")
                ?? new Response<StocksBySymbolResponse>(null, 400, "Falha ao obter ativo");

        Task<PagedResponse<List<Stock>>> IStockHandler.GetAllStocksAsync(GetAllStocksRequest request)
        {
            throw new NotImplementedException();
        }
    }
}