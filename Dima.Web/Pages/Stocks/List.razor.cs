using Dima.Core.Handlers;
using Dima.Core.Requests.Stocks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using stocks.Models;

namespace Dima.Web.Pages.Stocks
{
    public partial class ListStocksPage : ComponentBase
    {

        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<StockData> StocksList { get; set; } = new();
        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services 
        [Inject]
        public IStockHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllStocksRequest();
                var result = await Handler.GetAllStocksAsync(request);
                if(result.IsSuccess)
                {
                    StocksList =  result.Data?.Stocks ?? [];
                }
                else
                {
                    Snackbar.Add(result.Message!, Severity.Error);
                }
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

        #region Methods

        public Func<StockData, bool> Filter => stock =>
        {

            if (stock.Stock.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (stock.Name.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        #endregion
    }
} 