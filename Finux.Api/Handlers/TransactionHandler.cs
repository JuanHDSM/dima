using System.IO.Compression;
using Finux.Core.Common.Extensions;
using Finux.Api.Data;
using Finux.Core.Enums;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Transactions;
using Finux.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreateAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    IsRecurring = request.IsRecurring,
                    RecurringType = request.RecurringType ?? null,
                    Title = request.Title,
                    Type = request.Type
                };
                
                if (!request.IsRecurring)
                {                
                    await context.Transactions.AddAsync(transaction);
                    await context.SaveChangesAsync();
                    return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso");
                }
                
                const int times = 24;
                switch (request.RecurringType)
                {
                    case ERecurringType.Weekly :
                        for (var i = 0; i < times * 2; i++)
                        {
                            var newTransaction = new Transaction
                            {
                                Amount = transaction.Amount,
                                CategoryId = transaction.CategoryId,
                                CreateAt = DateTime.Now,   
                                IsRecurring = transaction.IsRecurring,
                                PaidOrReceivedAt = transaction.PaidOrReceivedAt = DateTime.Now.AddDays(i * 7),
                                RecurringType = transaction.RecurringType,
                                Title = transaction.Title,
                                Type = transaction.Type,
                                UserId = transaction.UserId
                            }; 
                            await context.Transactions.AddAsync(newTransaction);
                            await context.SaveChangesAsync();
                        }
                        break;
                    case ERecurringType.Monthly:
                        for (var i = 0; i < times; i++)
                        {
                                var newTransaction = new Transaction
                                {
                                    Amount = transaction.Amount,
                                    CategoryId = transaction.CategoryId,
                                    CreateAt = DateTime.Now,   
                                    IsRecurring = transaction.IsRecurring,
                                    PaidOrReceivedAt = transaction.PaidOrReceivedAt?.AddMonths(i),
                                    RecurringType = transaction.RecurringType,
                                    Title = transaction.Title,
                                    Type = transaction.Type,
                                    UserId = transaction.UserId
                                };   
                                await context.Transactions.AddAsync(newTransaction);
                                await context.SaveChangesAsync();
                            
                        }
                        break;
                    case ERecurringType.Yearly:
                        for (var i = 0; i < times/2 ; i++)
                        {
                            transaction.CreateAt = DateTime.Now.AddYears(i);
                            var newTransaction = new Transaction
                            {
                                Amount = transaction.Amount,
                                CategoryId = transaction.CategoryId,
                                CreateAt = DateTime.Now,   
                                IsRecurring = transaction.IsRecurring,
                                PaidOrReceivedAt = transaction.PaidOrReceivedAt?.AddYears(1),
                                RecurringType = transaction.RecurringType,
                                Title = transaction.Title,
                                Type = transaction.Type,
                                UserId = transaction.UserId
                            }; 
                            await context.Transactions.AddAsync(newTransaction);
                            await context.SaveChangesAsync();
                        }
                        break;
                    
                }

                return new Response<Transaction?>(transaction, 201, "Transações criadas com sucesso");
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível criar a transação");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions
                    .FirstOrDefaultAsync(x => 
                        x.Id == request.Id 
                        && x.UserId == request.UserId
                    );

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação excluida com sucesso");
            }
            catch 
            {
                return new Response<Transaction?>(null, 500, "Não foi possível excluir a transação");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x =>
                        x.Id == request.Id && x.UserId == request.UserId
                    );

                return transaction is null 
                    ? new Response<Transaction?>(null, 400, "Transação não encontrada") 
                    : new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Falha ao obter a transação");
            }

            
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter a data de início ou de termino");
            }

            try
            {
                var query = context.Transactions
                    .AsNoTracking()
                    .Where(x 
                        => x.PaidOrReceivedAt >= request.StartDate 
                           && x.PaidOrReceivedAt <= request.EndDate 
                           && x.UserId == request.UserId)
                    .Include(x => x.Category)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions, 
                    count, 
                    request.PageNumber, 
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível consultar as transações");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetExpenseByPeriod(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter a data de início ou de termino");
            }

            try
            {
                var query = context.Transactions
                    .AsTracking()
                    .Where(x 
                        => x.PaidOrReceivedAt >= request.StartDate 
                           && x.PaidOrReceivedAt <= request.EndDate 
                           && x.Type == ETransactionType.Withdraw 
                           && x.UserId == request.UserId)
                    .Include(x => x.Category)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var expensesTransaction =
                    await query
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync();
                
                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    expensesTransaction,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível consultar as transações");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetIncomesByPeriod(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter a data de início ou de termino");
            }

            try
            {
                var query = context.Transactions
                    .AsNoTracking()
                    .Where(x 
                        => x.PaidOrReceivedAt >= request.StartDate
                        && x.PaidOrReceivedAt <= request.EndDate
                        && x.Type == ETransactionType.Deposit
                        && x.UserId == request.UserId)
                    .Include(x => x.Category)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var incomesTransactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                
                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    incomesTransactions,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível consultar as transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId
                    );

                if (transaction is null)
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transação atualizada com sucesso");
            }
            catch 
            {
                return new Response<Transaction?>(null, 500, "Falha ao atualizar a transação");
            }
        }
    }
}