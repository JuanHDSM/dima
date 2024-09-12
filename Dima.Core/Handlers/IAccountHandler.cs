using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<Response<string>> LoginAsync(AuthRequest request);
        Task<Response<string>> RegisterAsync(AuthRequest request);
        Task LogoutAsync();
    }
}