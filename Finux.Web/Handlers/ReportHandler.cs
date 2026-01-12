using System.Net.Http.Json;
using Finux.Core.Handlers;
using Finux.Core.Models.Reports;
using Finux.Core.Requests.Reports;
using Finux.Core.Responses;

namespace Finux.Web.Handlers
{
    public class ReportHandler(IHttpClientFactory httpClientFactory) : IReportHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
            => await _client.GetFromJsonAsync<Response<List<ExpensesByCategory>?>>("api/v1/reports/expenses")
                ?? new Response<List<ExpensesByCategory>?> (null, 400, "Falha ao obter despesas por categoria.");

        public async Task<Response<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
            => await _client.GetFromJsonAsync<Response<FinancialSummary?>>("api/v1/reports/summary")
                ?? new Response<FinancialSummary?>(null, 400, "Falha ao obter resummo financeiro.");

        public async Task<Response<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
            => await _client.GetFromJsonAsync<Response<List<IncomesAndExpenses>?>>("api/v1/reports/incomes-expenses")
                ?? new Response<List<IncomesAndExpenses>?>(null, 400, "Falha ao obter entradas e saídas.");

        public async Task<Response<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
            => await _client.GetFromJsonAsync<Response<List<IncomesByCategory>?>>("api/v1/reports/incomes")
                ?? new Response<List<IncomesByCategory>?>(null, 400, "Falha ao obter entradas por categoria.");
    }
}