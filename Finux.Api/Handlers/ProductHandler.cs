using Finux.Api.Data;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Handlers;

public class ProductHandler(AppDbContext context) : IProductHandler
{
    public async Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request)
    {
        try
        {
            var query = context
                .Products
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Title);
            
            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            
            var countAsync = await query.CountAsync();
            
            return new PagedResponse<List<Product>?>(products, countAsync, request.PageNumber, request.PageSize);
        }
        catch
        {
            return new PagedResponse<List<Product>?>(null, 500, "Não foi possível consultar os produtos");
        }
    }

    public async Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request)
    {
        try
        {
            var product = await context
                .Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Slug == request.Slug && x.IsActive == true);

            return product is null
                ? new Response<Product?>(null, 404, "Produto não encontrado")
                : new Response<Product?>(product);
        }
        catch 
        {
            return new Response<Product?>(null, 500, "Não foi possível recuperar o produto");
        }
    }
}