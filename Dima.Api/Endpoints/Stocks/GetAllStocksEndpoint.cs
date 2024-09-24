using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using stocks.Responses;

namespace stocks.Endpoints
{
    public class GetAllStocksEndpoints : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("quote/list", HandleAsync)
                .WithName("Stocks: Get All Stocks")
                .WithSummary("Obtem todos os ativos")
                .WithDescription("Obtem todos os ativos")
                .WithOrder(1)
                .Produces<Response<StockResponse>>();
        }

        private static async Task<IResult> HandleAsync(
            IStockHandler handler
        )
        {
            var request = new GetAllStocksRequest();
            var result = await handler.GetAllStocksAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
