using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Dima.Core.Responses.Stocks;

namespace Dima.Api.Endpoints.Stocks
{
    public class GetStocksBySymbolExternalEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/quote/{symbol}", HandleAsync)
                .WithName("Stocks: Get Stocks By Symbol")
                .WithSummary("Obtem ativo por símbolo")
                .WithDescription("Obtem ativo por símbolo")
                .WithOrder(2)
                .Produces<Response<StocksBySymbolResponse>>(); ;
        }
        private static async Task<IResult> HandleAsync(
            IStockHandler handler,
            string symbol
        )
        {
            var request = new GetStockBySymbolRequest
            {
                Symbol = symbol
            };
            var result = await handler.GetStocksBySymbolExternalAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}