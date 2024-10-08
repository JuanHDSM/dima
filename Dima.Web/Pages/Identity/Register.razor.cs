using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Pages.Identity
{
    public partial class RegisterPage : ComponentBase
    {
        #region Properties
        public bool IsBusy { get; set; } = false;
        public AuthRequest InputModel { get; set; } = new();

        public bool _isShow;
        public InputType PasswordInput = InputType.Password;
        public string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IAccountHandler Handler { get; set; } = null!;
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
        #endregion

        #region Overrides
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if(user.Identity is { IsAuthenticated: true })
                NavigationManager.NavigateTo("/");
        }
        #endregion

        #region Methods
        public async Task OnValidSubmitAsync()
        {
            IsBusy = true;
            try
            {
                var result = await Handler.RegisterAsync(InputModel);

                if (result.IsSuccess)
                {
                    Snackbar.Add(result.Message!, Severity.Success);
                    NavigationManager.NavigateTo("/login");
                }
                else
                    Snackbar.Add(result.Message!, Severity.Error);
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

        public void ShowPassword()
        {
            if(_isShow)
            {
                _isShow = false;
                PasswordInput = InputType.Password;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            }
            else
            {
                _isShow = true;
                PasswordInput = InputType.Text;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
            }
        }

        #endregion
    }
}