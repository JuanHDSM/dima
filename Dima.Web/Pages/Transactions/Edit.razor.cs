using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Transactions
{
    public partial class EditTransactionPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public UpdateTransactionRequest InputModel { get; set; } = new();
        public List<Category> Categories { get; set; } = [];

        #endregion

        #region Parameters

        [Parameter]
        public string Id { get; set; } = string.Empty;

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;
        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;
            await GetTransactionByIdAsync();
            await GetAllCategoriesAsync();

        }

        #endregion

        #region Methods

        public async Task OnValidSubmitAsync()
        {
            try
            {
                IsBusy = true;
                var result = await Handler.UpdateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message!, Severity.Success);
                    NavigationManager.NavigateTo("/entry/history");
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

        #region Private Methods

        private async Task GetAllCategoriesAsync()
        {
            try
            {
                var categoryRequest = new GetAllCategoriesRequest();
                var result = await CategoryHandler.GetAllAsync(categoryRequest);
                if (result.IsSuccess)
                    Categories = result.Data ?? [];
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        private async Task GetTransactionByIdAsync()
        {

            GetTransactionByIdRequest? request = null;
            try
            {
                request = new GetTransactionByIdRequest { Id = long.Parse(Id) };
            }
            catch
            {
                Snackbar.Add("Parâmetro inválido", Severity.Error);
            }

            if (request is null)
                return;

            try
            {
                var result = await Handler.GetByIdAsync(request!);
                if (result.IsSuccess && result.Data is not null)
                    InputModel = new UpdateTransactionRequest
                    {
                        Id = result.Data.Id,
                        Title = result.Data.Title,
                        Type = result.Data.Type,
                        Amount = result.Data.Amount,
                        CategoryId = result.Data.CategoryId,
                        PaidOrReceivedAt = result.Data.PaidOrReceivedAt
                    };
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