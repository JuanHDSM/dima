using System.Net.Http.Json;
using Finux.Core.Handlers;
using Finux.Core.Requests.Stripe;
using Finux.Core.Responses;
using Finux.Core.Responses.Stripe;

namespace Finux.Web.Handlers;

public class StripeHandler(IHttpClientFactory httpClientFactory) : IStripeHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request)
    {
        var result = await _client.PostAsJsonAsync($"api/v1/payments/stripe/session", request);
        return await result.Content.ReadFromJsonAsync<Response<string?>>()
               ?? new Response<string?>(null, 400, "Falha ao criar seção no stripe");
    }

    public async Task<Response<List<StripeTransactionResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request)
    {
        var result = await _client.PostAsJsonAsync($"api/v1/payments/stripe/{request.Number}/transactions", request);
        return await result.Content.ReadFromJsonAsync<Response<List<StripeTransactionResponse>>>()
               ?? new Response<List<StripeTransactionResponse>>(null, 400, "Falha ao consultar transações do pedido");
        
    }
}