using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models.Reports;
using Finux.Core.Requests.Reports;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Reports
{
    public class GetIncomesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/incomes", HandleAsync)
                .WithName("Incomes: Get Expenses By Category")
                .WithSummary("Obtem entrada por categoria")
                .WithDescription("Obtem entrada por categoria")
                .WithOrder(4)
                .Produces<Response<List<IncomesByCategory>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IReportHandler handler
        )
        {
            var request = new GetIncomesByCategoryRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };
            
            var result = await handler.GetIncomesByCategoryReportAsync(request);

            return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
        }
    }
}