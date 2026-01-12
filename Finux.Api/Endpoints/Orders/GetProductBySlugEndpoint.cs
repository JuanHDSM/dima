using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finux.Api.Endpoints.Orders;

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