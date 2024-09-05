using Dima.Api.Common.Api;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Endpoints.Identity
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/logout", HAndleAsync).RequireAuthorization();
        

        private static async Task<IResult> HAndleAsync(
            SignInManager<User> signInManager
        )
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }
}