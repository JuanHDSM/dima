using System.Security.Claims;
using System.Transactions;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Requests.Transactions;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("Transactions: Get Transaction By Id")
                .WithSummary("Obtem uma transação")
                .WithDescription("Obtem uma transação")
                .WithOrder(4)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            long id
        )
        {
            var request = new GetTransactionByIdRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}