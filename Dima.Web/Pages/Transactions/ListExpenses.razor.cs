using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions;

public partial class ListExpensesPage : ComponentBase
{
    #region  Properties

    public bool IsBusy { get; set; } = false;
    
    public string SearchTerm { get; set; } = string.Empty;
    
    public List<Transaction> Transactions { get; set; } = new();
    
    public int CurrentMonth { get; set; } = DateTime.Now.Month;
    
    public int CurrentYear { get; set; } = DateTime.Now.Year;

    public int[] Years { get; set; } =
    {
        DateTime.Now.Year,
        DateTime.Now.AddYears(-1).Year,
        DateTime.Now.AddYears(-2).Year,
        DateTime.Now.AddYears(-3).Year,
        DateTime.Now.AddYears(-4).Year,
    };

    #endregion

    #region Services

    public ISnackbar Snackbar { get; set; } = null!;
    
    public ITransactionHandler Handler { get; set; } = null!;

    public IDialogService DialogService { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async void OnInitialized()
        => await GetExpensesAsync();

    #endregion

    #region Methods

    

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