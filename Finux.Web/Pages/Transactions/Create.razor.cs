using Finux.Core.Enums;
using Finux.Core.Handlers;
using Finux.Core.Models;
using Finux.Core.Requests.Categories;
using Finux.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Finux.Web.Pages.Transactions
{
    public partial class CreateTransactionPage : ComponentBase
    {


        #region Properties

        public bool IsBusy { get; set; } = false;
        public bool IsRecurring { get; set; } = false;

        public CreateTransactionRequest InputModel { get; set; } = new();

        public string _value1 { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = [];

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public ITransactionHandler Handler { get; set; } = null!;
        [Inject]
        public ICategoryHandler CategoryHandler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await CategoryHandler.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    Categories = result.Data ?? [];
                    InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0; 
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

        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                if (_value1 == "total_values")
                {
                    InputModel.Amount /= InputModel.Installments ?? 1;
                }
                var result = await Handler.CreateAsync(InputModel);
                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message!, Severity.Success);
                    if (InputModel.Type == ETransactionType.Withdraw)
                    {
                        NavigationManager.NavigateTo("/entry/expenses/history");
                    }
                    else
                    {
                        NavigationManager.NavigateTo("/entry/incomes/history");
                    }
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
    }
}