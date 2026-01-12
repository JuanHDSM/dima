using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Orders;

public class CreateOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Order: Create Order")
            .WithSummary("Cria um pedido")
            .WithDescription("Cria um pedido")
            .WithOrder(3)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        CreateOrderRequest request,
        ClaimsPrincipal user
    )
    {
        request.UserId = user.Identity!.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);

        return result.IsSuccess
            ? TypedResults.Created( $"/{result.Data?.Number}",result)
            : TypedResults.BadRequest(result);
    }
}