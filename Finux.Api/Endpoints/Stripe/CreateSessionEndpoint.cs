using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Stripe;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Stripe;

public class CreateSessionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/session", HandleAsync)
            .WithName("Session: Create Session")
            .WithSummary("Cria uma seção")
            .WithDescription("Cria uma seção")
            .WithOrder(1)
            .Produces<Response<string?>>();
    

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IStripeHandler handler,
        CreateSessionRequest request
        )
    {
        request.UserId = user.Identity!.Name ?? string.Empty; 
        
        var result = await handler.CreateSessionAsync(request);
        return result.IsSuccess 
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}