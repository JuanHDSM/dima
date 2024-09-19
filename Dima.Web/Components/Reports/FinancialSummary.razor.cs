using Dima.Core.Handlers;
using Dima.Core.Requests.Reports;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Dima.Web.Components.Reports
{
    public partial class FinancialSummaryComponent : ComponentBase
    {
        #region Properties
        public bool ShowValues { get; set; } = true;
        public Core.Models.Reports.FinancialSummary? Summary { get; set; }
        #endregion

        #region Services
        [Inject]
        public ISnackbar Snackbar { get; set; } = null!;
        [Inject]
        public IReportHandler Handler { get; set; } = null!;

        #endregion

        #region Overrides

        protected override async Task OnInitializedAsync()
        {
            var request = new GetFinancialSummaryRequest();
            var result = await Handler.GetFinancialSummaryReportAsync(request);
            if (result.IsSuccess)
                Summary = result.Data;
        }

        #endregion

        #region Methods

        public void ToggleShowValues() 
            => ShowValues = !ShowValues;

        #endregion
    }
}