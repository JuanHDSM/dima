using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Orders;

public class GetProductBySlugEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{slug}", HandleAsync)
            .WithName("Order: Get Product By Slug")
            .WithSummary("Obter um produto pelo slug")
            .WithDescription("Obter um produto pelo slug")
            .WithOrder(1)
            .Produces<Response<Product?>>();

    private static async Task<IResult> HandleAsync(
        IProductHandler handler,
        ClaimsPrincipal user,
        string slug
    )
    {
        var request = new GetProductBySlugRequest
        {
            UserId = user.Identity!.Name ?? string.Empty,
            Slug = slug
        };
        
        var result = await handler.GetBySlugAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}