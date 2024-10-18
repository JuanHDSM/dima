using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Stocks;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Stocks
{
    public class GetAllStocksEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Stocks: Get All Stocks By User")
                .WithSummary("Obtem todos os ativos do usuário")
                .WithDescription("Obtem todos os ativos do usuário")
                .WithOrder(4)
                .Produces<PagedResponse<List<Stock>>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IStockHandler handler,
            [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetAllStocksRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllStocksAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}