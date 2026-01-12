using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Stocks;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Stocks
{
    public class CreateStockEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {

            app.MapPost("/quote", HandleAsync)
                .WithName("Stocks: Create Stocks")
                .WithSummary("Cria um novo ativo")
                .WithDescription("Cria um novo ativo")
                .WithOrder(3)
                .Produces<Response<Stock>>();
        }

        private static async Task<IResult> HandleAsync(
            IStockHandler handler,
            ClaimsPrincipal user,
            CreateStockRequest request
        )
        {
            request = new CreateStockRequest
            {
                UserId = user.Identity!.Name ?? string.Empty,
                Symbol = request.Symbol
            };

            var result = await handler.CreateStockAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Symbol}", result)
                : TypedResults.BadRequest(result);
        }
    }
}