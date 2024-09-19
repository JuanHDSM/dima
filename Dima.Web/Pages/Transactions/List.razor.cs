using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions
{
    public partial class ListTransactionPage : ComponentBase
    {
        #region Properties 

        public bool IsBusy { get; set; } = false;

        public List<Transaction> Transactions { get; set; } = new();

        public string SearchTerm { get; set; } = string.Empty;
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public int CurrentMonth { get; set; } = DateTime.Now.Month;

        public int[] Years { get; set; } =
        {
            DateTime.Now.Year,
            DateTime.Now.AddYears(-1).Year,
            DateTime.Now.AddYears(-2).Year,
            DateTime.Now.AddYears(-3).Year
        };

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
            => await GetTransactionsAsync();

        #endregion

        #region Methods

        public async Task OnSearchAsync()
        { 
            await GetTransactionsAsync();
            StateHasChanged();
        }


        public Func<Transaction, bool> Filter => transaction =>
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return true;

            if (transaction.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (transaction.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (transaction.Type.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (transaction.Amount.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (transaction.Category.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (transaction.PaidOrReceivedAt.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogService.ShowMessageBox(
                "Atenção",
                $"Ao presseguir com essa ação você irá excluir a transação {title}. Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "CANCELAR"
            );
            if (result is true)
                await OnDeleteAsync(id, title);

            StateHasChanged();
        }

        #endregion

        #region Private Methods

        private async Task OnDeleteAsync(long id, string title)
        {
            try
            {
                var request = new DeleteTransactionRequest { Id = id };
                await Handler.DeleteAsync(request);
                Transactions.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Transação {title} excluída com sucesso", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);

            }
        }

        private async Task GetTransactionsAsync()
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

                var result = await Handler.GetByPeriodAsync(request);
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