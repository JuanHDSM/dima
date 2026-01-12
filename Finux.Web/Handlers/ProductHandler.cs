using System.Net.Http.Json;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Web.Handlers;

public class ProductHandler(IHttpClientFactory httpClientFactory) : IProductHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request)
     => await _client.GetFromJsonAsync<PagedResponse<List<Product>?>>("api/v1/products")
        ?? new PagedResponse<List<Product>?>(null, 400, "Não foi possóver obter os produtos");

    public async Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request)
     => await _client.GetFromJsonAsync<Response<Product?>>($"api/v1/products/{request.Slug}")
            ?? new Response<Product?>(null, 400, "Não foi possível obter o produto");
}