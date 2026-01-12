using System.Security.Claims;
using System.Transactions;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Requests.Transactions;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
                .WithName("Transactions: Update")
                .WithSummary("Atualiza uma transação")
                .WithDescription("Atualiza uma transação")
                .WithOrder(2)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            UpdateTransactionRequest request,
            long id
        )
        {
            request.Id = id;
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.UpdateAsync(request);

            return result.IsSuccess 
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
            
        }
    }
}