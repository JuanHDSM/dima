using Finux.Core.Handlers;
using Finux.Core.Models.Stocks;
using Finux.Core.Requests.Stocks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Finux.Web.Pages.Stocks
{
    public partial class WalletPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;
        public List<AssetsInWallet> AssetsInWallet { get; set; } = new();

        #endregion

        #region Services

        [Inject]
        public IStockHandler Handler { get; set; } = null!;
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        #endregion 

        #region Ovverides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            var request = new GetAssetsInWalletRequest();
            try
            {
                var result = await Handler.GetAssetsInWalletAsync(request);
                if (result.IsSuccess)
                    AssetsInWallet = result.Data ?? [];
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