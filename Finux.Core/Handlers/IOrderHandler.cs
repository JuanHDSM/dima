using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Core.Handlers;

public interface IOrderHandler
{
    Task<Response<Order?>> CancelAsync(CancelOrderRequest request);
    Task<Response<Order?>> CreateAsync(CreateOrderRequest request);
    Task<Response<Order?>> PayAsync(PayOrderRequest request);
    Task<Response<Order?>> RefundAsync(RefundOrderRequest request);
    Task<PagedResponse<List<Order>?>> GetAllAsync(GetAllOrderRequest request);
    Task<Response<Order?>> GetByNumberAsync(GetOrderByNumberRequest request);
}