using Finux.Core.Models;
using Finux.Core.Requests.Transactions;
using Finux.Core.Responses;

namespace Finux.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request);
        Task<PagedResponse<List<Transaction>?>> GetExpenseByPeriod(GetTransactionsByPeriodRequest request);
        Task<PagedResponse<List<Transaction>?>> GetIncomesByPeriod(GetTransactionsByPeriodRequest request);
    }
}