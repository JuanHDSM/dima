using Finux.Core.Models.Stocks;
using Finux.Core.Responses;
using Finux.Core.Handlers;
using Finux.Core.Requests.Stocks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using stocks.Models;

namespace Finux.Web.Pages.Stocks
{
    public partial class CreateStockPage : ComponentBase
    {

        #region Properties

        public bool IsBusy { get; set; } = false;
        public CreateStockRequest InputModel { get; set; } = new();
        public StockData Stock { get; set; } = new();
        public List<StockData> ExternalStocks { get; set; } = new();

        #endregion

        #region Services
        [Inject]
        public IStockHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            try
            {
                var request = new GetAllStocksRequest();
                var result = await Handler.GetAllStocksExternalAsync(request);
                ExternalStocks = result.Data!.Stocks;
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Handler.CreateStockAsync(InputModel);
                if(result.IsSuccess)
                {
                    Snackbar.Add(result.Message!, Severity.Success);
                    NavigationManager.NavigateTo("/wallet");
                }
                else
                {
                    Snackbar.Add(result.Message!, Severity.Error);
                }
            }
            catch (Exception e)
            {
                Snackbar.Add(e.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}