using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Orders;

public class RefundOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{id:long}/refund", HandleAsync)
            .WithName("Order: Refund An Order")
            .WithSummary("Estorna um pedido")
            .WithDescription("Estorna um pedido")
            .WithOrder(6)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        ClaimsPrincipal user,
        long id
    )
    {
        var request = new RefundOrderRequest
        {
            Id = id,
            UserId = user.Identity!.Name ?? string.Empty,
        };
        
        var result = await handler.RefundAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok( result)
            : TypedResults.BadRequest(result);
    }
}