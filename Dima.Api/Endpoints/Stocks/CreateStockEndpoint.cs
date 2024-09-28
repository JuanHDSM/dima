using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;

namespace Dima.Api.Endpoints.Stocks
{
    public class CreateStockEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {

            app.MapPost("/quote", HandleAsync);
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