using Finux.Core.Models;
using Finux.Core.Requests.Orders;
using Finux.Core.Responses;

namespace Finux.Core.Handlers;

public interface IVoucherHandler
{
    Task<Response<Voucher?>> GetByNumberAsync(GetVoucherByNumberRequest request);
}