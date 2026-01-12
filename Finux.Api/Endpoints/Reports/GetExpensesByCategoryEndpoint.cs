using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models.Reports;
using Finux.Core.Requests.Reports;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Reports
{
    public class GetExpensesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/expenses", HandleAsync)
                .WithName("Expenses: Get Expenses By Category")
                .WithSummary("Obtem despesas por categoria")
                .WithDescription("Obtem despesas por categoria")
                .WithOrder(1)
                .Produces<Response<List<ExpensesByCategory>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler
        )
        {
            var request = new GetExpensesByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            
            var result = await handler.GetExpensesByCategoryReportAsync(request);

            return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
        }
    }
}