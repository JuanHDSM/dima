using Dima.Api.Data;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Dima.Core.Responses.Stocks;
using stocks.Responses;

namespace Dima.Api.Handlers
{
    public class StockHandler(IHttpClientFactory httpClientFactory, AppDbContext context) : IStockHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.StocksHttpClientName);

        public async Task<Response<Stock>> CreateStockAsync(CreateStockRequest request)
        {

            try
            {
                var result = await _client.GetFromJsonAsync<StocksBySymbolResponse>($"/api/quote/{request.Symbol}?token={Configuration.TokenStockService}");

                if (result?.Results is null)
                {
                    return new Response<Stock>(null, 404, $"Ativo {request.Symbol.ToUpper()} não encontrado.");
                }

                var stock = result.Results.FirstOrDefault();

                var newStock = new Stock
                {
                    Currency = stock!.Currency,
                    ShortName = stock.ShortName,
                    LongName = stock.LongName,
                    RegularMarketChange = stock.RegularMarketChange,
                    RegularMarketChangePercent = stock.RegularMarketChangePercent,
                    RegularMarketTime = stock.RegularMarketTime,
                    RegularMarketPrice = stock.RegularMarketPrice,
                    RegularMarketDayHigh = stock.RegularMarketDayHigh,
                    RegularMarketDayRange = stock.RegularMarketDayRange,
                    RegularMarketDayLow = stock.RegularMarketDayLow,
                    RegularMarketVolume = stock.RegularMarketVolume,
                    RegularMarketPreviousClose = stock.RegularMarketPreviousClose,
                    RegularMarketOpen = stock.RegularMarketOpen,
                    FiftyTwoWeekRange = stock.FiftyTwoWeekRange,
                    FiftyTwoWeekLow = stock.FiftyTwoWeekLow,
                    FiftyTwoWeekHigh = stock.FiftyTwoWeekHigh,
                    Symbol = stock.Symbol,
                    PriceEarnings = stock.PriceEarnings,
                    EarningsPerShare = stock.EarningsPerShare,
                    LogoUrl = stock.LogoUrl,
                    UserId = request.UserId
                };
                
                await context.Stocks.AddAsync(newStock);
                await context.SaveChangesAsync();

                return new Response<Stock>(newStock, 201, "Ativo criado com sucesso.");
            }
            catch
            {
                return new Response<Stock>(null, 400, "Falha ao criar ativo.");
            }

        }

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