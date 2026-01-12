using Finux.Api.Data;
using Finux.Core;
using Finux.Core.Handlers;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Stocks;
using Finux.Core.Responses;
using Finux.Core.Responses.Stocks;
using Microsoft.EntityFrameworkCore;
using stocks.Responses;

namespace Finux.Api.Handlers
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

        public async Task<PagedResponse<List<Stock>>> GetAllStocksAsync(GetAllStocksRequest request)
        {
            try
            {
                var query = context.Stocks
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Id);

                var result = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Stock>>(
                        result, 
                        count, 
                        request.PageNumber, 
                        request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Stock>>(null, 500, "Não foi possível consultar os ativos.");
            }
        }

        public async Task<Response<StockResponse>> GetAllStocksExternalAsync(GetAllStocksRequest request)
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

        public async Task<Response<List<AssetsInWallet>>> GetAssetsInWalletAsync(GetAssetsInWalletRequest request)
        {
            try
            {
                var data = await context.AssetsInWallets
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Symbol)
                    .ToListAsync();

                return new Response<List<AssetsInWallet>>(data);
            }
            catch
            {
                return new Response<List<AssetsInWallet>>(null, 400, "Falha ao obter ativos da carteira");
            }
        }

        public async Task<Response<StocksBySymbolResponse>> GetStocksBySymbolExternalAsync(GetStockBySymbolRequest request)
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