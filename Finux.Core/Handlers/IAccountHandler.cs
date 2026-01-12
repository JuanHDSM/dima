using Finux.Core.Requests.Account;
using Finux.Core.Responses;

namespace Finux.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<Response<string>> LoginAsync(AuthRequest request);
        Task<Response<string>> RegisterAsync(AuthRequest request);
        Task LogoutAsync();
    }
}