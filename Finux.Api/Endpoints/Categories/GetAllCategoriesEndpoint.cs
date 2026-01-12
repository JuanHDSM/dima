using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Categories;
using Finux.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finux.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("Categories: Get All Categories")
                .WithSummary("Obtem todas as categorias de um usuário")
                .WithDescription("Obtem todas as categorias de um usuário")
                .WithOrder(5)
                .Produces<PagedResponse<List<Category>?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
            [FromQuery]int pageSize = Configuration.DefaultPageSize
        )
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);
            return result.IsSuccess 
                ? TypedResults.Ok(result) 
                : TypedResults.BadRequest(result);
        }
    }
}