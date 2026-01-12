using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Core.Handlers;

public interface IProductHandler
{
    Task<PagedResponse<List<Product>?>> GetAllAsync(GetAllProductsRequest request);
    Task<Response<Product?>> GetBySlugAsync(GetProductBySlugRequest request);
}