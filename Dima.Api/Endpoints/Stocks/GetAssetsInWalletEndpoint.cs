using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models.Stocks;
using Dima.Core.Requests.Stocks;

namespace Dima.Api.Endpoints.Stocks
{
    public class GetAssetsInWalletEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/wallet", HandleAsync)
                .Produces<AssetsInWallet>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IStockHandler handler
        )
        {
            var request = new GetAssetsInWalletRequest
            {
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetAssetsInWalletAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}