using System.Security.Claims;
using Finux.Api.Common.Api;
using Finux.Core.Handlers;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Stocks;

namespace Finux.Api.Endpoints.Stocks
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