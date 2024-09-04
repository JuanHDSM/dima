using System.Transactions;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions: Get Transaction By Id")
                .WithSummary("Obtem uma transação")
                .WithDescription("Obtem uma transação")
                .WithOrder(4)
                .Produces<Response<Transaction?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            long id
        )
        {
            var request = new GetTransactionByIdRequest
            {
                Id = id,
                UserId = "holy@holy.io"
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}