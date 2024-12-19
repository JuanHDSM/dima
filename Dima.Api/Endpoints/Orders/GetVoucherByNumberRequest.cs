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

public class GetVoucherByNumberEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{number}", HandleAsync)
            .WithName("Order: Get Voucher By Number")
            .WithSummary("Obter um voucher pelo número")
            .WithDescription("Obter um voucher pelo número")
            .WithOrder(1)
            .Produces<Response<Voucher?>>();

    private static async Task<IResult> HandleAsync(
        IVoucherHandler handler,
        ClaimsPrincipal user,
        string number
    )
    {
        var request = new GetVoucherByNumberRequest
        {
            UserId = user.Identity!.Name ?? string.Empty,
            Number = number
        };
        
        var result = await handler.GetByNumberAsync(request);

        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}