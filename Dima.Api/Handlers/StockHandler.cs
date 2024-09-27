using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Dima.Core.Responses.Stocks;
using stocks.Responses;

namespace Dima.Api.Handlers
{
    public class StockHandler(IHttpClientFactory httpClientFactory) : IStockHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.StocksHttpClientName);
        public async Task<Response<StockResponse>> GetAllStocksAsync(GetAllStocksRequest request)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<StockResponse>($"/api/quote/list?token={Configuration.TokenStockService}");
                return new Response<StockResponse>(result);
            }
            catch
            {
                return new Response<StockResponse>(null, 400, "Falha ao obter ativos.");
            }
        }

        public async Task<Response<StocksBySymbolResponse>> GetStocksBySymbolAsync(GetStockBySymbolRequest request)
        {
            try
            {
                var result = await _client.GetFromJsonAsync<StocksBySymbolResponse>($"/api/quote/{request.Symbol.ToUpper()}?token={Configuration.TokenStockService}");
                return new Response<StocksBySymbolResponse>(result);
            }
            catch
            {
                return new Response<StocksBySymbolResponse>(null, 400, "Falha ao obter ativo.");
                throw;
            }
        }
    }
}