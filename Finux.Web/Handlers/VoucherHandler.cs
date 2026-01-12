using System.Net.Http.Json;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Web.Handlers;

public class VoucherHandler(IHttpClientFactory httpClientFactory) : IVoucherHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request)
        => await _client.GetFromJsonAsync<Response<Voucher?>>($"api/v1/vouchers/{request.Number}")
           ?? new Response<Voucher?>(null, 400, "Não foi possível obter ao voucher");
}