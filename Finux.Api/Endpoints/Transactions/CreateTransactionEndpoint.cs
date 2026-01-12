using System.Security.Claims;
using System.Transactions;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Requests.Transactions;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("Transactions: Create")
                .WithSummary("Criar uma nova transação")
                .WithDescription("Criar uma nova transação")
                .WithOrder(1)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            CreateTransactionRequest request
        )
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.CreateAsync(request);
            return result.IsSuccess
                ? TypedResults.Created($"/{result.Data?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}