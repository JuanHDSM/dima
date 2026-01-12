using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Categories;
using Finux.Core.Responses;

namespace Finux.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("Categories: Get Category By Id")
                .WithSummary("Obtem uma categoria por id")
                .WithDescription("Obtem uma categoria por id")
                .WithOrder(4)
                .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            long id
        )
        {
            var request = new GetCategoryByIdRequest
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