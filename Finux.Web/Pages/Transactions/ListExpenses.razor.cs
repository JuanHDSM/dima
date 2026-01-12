using Finux.Core.Common.Extensions;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Finux.Web.Pages.Transactions
{

    public partial class ListExpensesPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;

        public string SearchTerm { get; set; } = string.Empty;

        public List<Transaction> Transactions { get; set; } = new();

        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public int CurrentYear { get; set; } = DateTime.Now.Year;

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
            => await GetExpensesAsync();

        #endregion

        #region Private Methods

        private async Task GetExpensesAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetTransactionsByPeriodRequest
                {
                    StartDate = DateTime.Now.GetFirstDay(CurrentYear, CurrentMonth),
                    EndDate = DateTime.Now.GetLastDay(CurrentYear, CurrentMonth),
                    PageNumber = 1,
                    PageSize = 1000
                };

                var result = await Handler.GetExpenseByPeriod(request);
                if (result.IsSuccess)
                    Transactions = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}