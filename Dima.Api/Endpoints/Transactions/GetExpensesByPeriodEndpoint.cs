using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetExpensesByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Transactions: Get Expenses By Period")
                .WithSummary("Obtem despesas de um determinado intervalo de datas")
                .WithDescription("Obtem depesas de um determinado intervalo de datas")
                .WithOrder(6)
                .Produces<PagedResponse<List<Transaction>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ITransactionHandler handler,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery]int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetTransactionsByPeriodRequest {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate
            };

            var result = await handler.GetExpenseByPeriod(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}