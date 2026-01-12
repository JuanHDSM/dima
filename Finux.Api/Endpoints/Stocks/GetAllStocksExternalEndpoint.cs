using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Requests.Stocks;
using Finux.Core.Responses;
using stocks.Responses;

namespace stocks.Endpoints
{
    public class GetAllStocksExternalEndpoint : IEndpoint
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
            var result = await handler.GetAllStocksExternalAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
