using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
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
            catch (Exception e)
            {
                return new Response<StockResponse>(null, 400, e.Data.ToString());
            }
        }
    }
}