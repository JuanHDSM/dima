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