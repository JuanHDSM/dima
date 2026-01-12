using System.Net.Http.Json;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Web.Handlers;

public class OrderHandler(IHttpClientFactory httpClientFactory) : IOrderHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Order?>> CancelAsync(CancelOrderRequest request)
    {
        var result = await _client.PostAsJsonAsync($"api/v1/orders/{request.Id}/cancel", request);
        return await result.Content.ReadFromJsonAsync<Response<Order?>>() 
            ?? new Response<Order?>(null, 400, "Não foi possível cancelar este pedido");
    }

    public async Task<Response<Order?>> CreateAsync(CreateOrderRequest request)
    {
        var result = await _client.PostAsJsonAsync("api/v1/orders", request);
        return await result.Content.ReadFromJsonAsync<Response<Order?>>() 
               ?? new Response<Order?>(null, 400, "Não foi possível cancelar este pedido");
    }

    public async Task<Response<Order?>> PayAsync(PayOrderRequest request)
    {
        var result = await _client.PostAsJsonAsync($"api/v1/orders/{request.Number}/pay", request);
        return await result.Content.ReadFromJsonAsync<Response<Order?>>() 
               ?? new Response<Order?>(null, 400, "Não foi possível pagar este pedido");
    }

    public async Task<Response<Order?>> RefundAsync(RefundOrderRequest request)
    {
        var result = await _client.PostAsJsonAsync($"api/v1/orders/{request.Id}/refund", request);
        return await result.Content.ReadFromJsonAsync<Response<Order?>>() 
               ?? new Response<Order?>(null, 400, "Não foi possível reembolsar este pedido");
    }

    public async Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrderRequest request)
        => await _client.GetFromJsonAsync<PagedResponse<List<Order>?>>("api/v1/orders")
           ?? new PagedResponse<List<Order>?>(null, 400, "Não foi possóver obter os pedidos");

    public async Task<Response<Order?>> GetByNumberAsync(GetOrderByNumberRequest request)
        => await _client.GetFromJsonAsync<Response<Order?>>($"api/v1/orders/{request.Number}")
           ?? new Response<Order?>(null, 400, "Não foi possível obter o produto");
}