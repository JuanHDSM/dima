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

public class GetAllProductsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Order: Get All Products")
            .WithSummary("Obter todos produtos")
            .WithDescription("Obter todos produtos")
            .WithOrder(1)
            .Produces<PagedResponse<List<Product>?>>();

    private static async Task<IResult> HandleAsync(
        IProductHandler handler,
        ClaimsPrincipal user,
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllProductsRequest
        {
            UserId = user.Identity!.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await handler.GetAllAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}