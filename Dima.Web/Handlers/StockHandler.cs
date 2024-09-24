using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using stocks.Responses;

namespace Dima.Web.Handlers
{
    public class StockHandler(IHttpClientFactory httpClientFactory) : IStockHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<StockResponse>> GetAllStocksAsync(GetAllStocksRequest request)
            => await _client.GetFromJsonAsync<Response<StockResponse>>("v1/stocks/quote/list")
                ?? new Response<StockResponse>(null, 400, "Falha ao obter ações");
    }
}