using System.Net.Http.Json;
using System.Text;
using Finux.Core.Handlers;
using Finux.Core.Requests.Account;
using Finux.Core.Responses;

namespace Finux.Web.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<string>> LoginAsync(AuthRequest request)
        {
            var result = await _client.PostAsJsonAsync("/api/v1/identity/login?useCookies=true", request);
            return result.IsSuccessStatusCode
            ? new Response<string>("Login realizado com sucesso", 200, "Login realizado com sucesso")
            : new Response<string>(null, 400, "Não foi possível realizar login");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
            await _client.PostAsJsonAsync("api/v1/identity/logout", new { });
        }

        public async Task<Response<string>> RegisterAsync(AuthRequest request)
        {
            var result = await _client.PostAsJsonAsync("api/v1/identity/register", request);
            return result.IsSuccessStatusCode
            ? new Response<string>("Cadastro realizado com sucesso", 201, "Cadastro realizado com sucesso")
            : new Response<string>(null, 400, "Não foi possível realizar cadastro");
        }
    }
}