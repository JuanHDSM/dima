using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Categories
{
    public partial class ListCategoriesPage : ComponentBase
    {
        #region Properties

        public bool IsBusy { get; set; } = false;

        public List<Category> Categories { get; set; } = [];

        public string SearchTerm { get; set; } = string.Empty;

        #endregion

        #region Services

        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;

        [Inject]
        public ICategoryHandler Handler { get; set; } = null!;

        [Inject]
        public IDialogService DialogService { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            IsBusy = true;

            try
            {
                var request = new GetAllCategoriesRequest();
                var result = await Handler.GetAllAsync(request);
                if (result.IsSuccess)
                    Categories = result.Data ?? [];
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

        public Func<Category, bool> Filter => category =>
        {
            if (string.IsNullOrEmpty(SearchTerm))
                return true;

            if (category.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (category.Title.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            if (category.Description is not null && category.Description!.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };

        public async void OnDeleteButtonClickedAsync(long id, string title)
        {
            var result = await DialogService.ShowMessageBox(
                "ATENÇÃO",
                $"Ao presseguir com essa ação você irá excluir a categoria {title}. Deseja continuar?",
                yesText: "EXCLUIR",
                cancelText: "CANCELAR"
            );

            if (result is true)
                await OnDeletAsync(id, title);

            StateHasChanged();
        }

        public async Task OnDeletAsync(long id, string title)
        {
            try
            {
                var request = new DeleteCategoryRequest { Id = id };
                await Handler.DeleteAsync(request);
                Categories.RemoveAll(x => x.Id == id);
                Snackbar.Add($"Categoria {title} excluída", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        #endregion
    }
}