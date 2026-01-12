using Finux.Core.Requests.Stripe;
using Finux.Core.Responses;
using Finux.Core.Responses.Stripe;

namespace Finux.Core.Handlers;

public interface IStripeHandler
{
    Task<Response<string?>> CreateSessionAsync(CreateSessionRequest request);
    Task<Response<List<StripeTransactionResponse>>> GetTransactionsByOrderNumberAsync(GetTransactionsByOrderNumberRequest request);
}