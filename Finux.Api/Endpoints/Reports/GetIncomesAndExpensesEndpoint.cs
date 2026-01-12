using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models.Reports;
using Finux.Core.Requests.Reports;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Reports
{
    public class GetIncomesAndExpensesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/incomes-expenses", HandleAsync)
                .WithName("Incomes And Expenses: Get Incomes And Expenses")
                .WithSummary("Obtem entrada e saídas")
                .WithDescription("Obtem um")
                .WithOrder(3)
                .Produces<Response<List<IncomesAndExpenses>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler
        )
        {
            var request = new GetIncomesAndExpensesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            var result = await handler.GetIncomesAndExpensesReportAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}