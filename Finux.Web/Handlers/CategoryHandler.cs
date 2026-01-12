using System.Net.Http.Json;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Categories;
using Finux.Core.Responses;

namespace Finux.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _client.PostAsJsonAsync("api/v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao criar a categoria");
        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _client.DeleteAsync($"api/v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao exluir a categoria");
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
            => await _client.GetFromJsonAsync<PagedResponse<List<Category>>>("api/v1/categories")
                ?? new PagedResponse<List<Category>>(null, 400, "Não foi possível obter as categorias");
        

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
            => await _client.GetFromJsonAsync<Response<Category?>>($"api/v1/categories/{request.Id}")
                ?? new Response<Category?>(null, 400, "Não foi possível obter a categoria");
        

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _client.PutAsJsonAsync($"api/v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Category?>>()
                ?? new Response<Category?>(null, 400, "Falha ao atualizar a categoria");
        }
    }
}