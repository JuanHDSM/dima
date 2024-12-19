using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Orders;

public class PayOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{number}/pay", HandleAsync)
            .WithName("Order: Pay An Order")
            .WithSummary("Paga um pedido")
            .WithDescription("Paga um pedido")
            .WithOrder(6)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        PayOrderRequest request,
        ClaimsPrincipal user,
        string number
    )
    {
        request.UserId = user.Identity!.Name ?? string.Empty;
        request.Number = number;
        
        var result = await handler.PayAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok( result)
            : TypedResults.BadRequest(result);
    }
}