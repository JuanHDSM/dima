using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Transactions: Get Transactions By Period")
                .WithSummary("Obtem transações de um determinado intervalo de datas")
                .WithDescription("Obtem transações de um determinado intervalo de datas")
                .WithOrder(5)
                .Produces<PagedResponse<List<Transaction>?>>();
        }

        private static async Task<IResult> HandleAsync(
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery]int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetTransactionsByPeriodRequest {
                UserId = "holy@holy.io",
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetByPeriodAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}