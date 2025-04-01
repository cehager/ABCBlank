using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Shared
{
    public partial class DeleteConfirmationDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public string Title { get; set; } = "Delete Confirmation";
        [Parameter]
        public string Message { get; set; } = "Are you sure you want to delete this item?";
        //[Parameter]
        //public string ConfirmButtonText { get; set; } = "Delete";
        //[Parameter]
        //public string CancelButtonText { get; set; } = "Cancel";
        //[Parameter]
        //public EventCallback OnConfirm { get; set; }

        void Agreed() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
        //private async Task ConfirmAsync()
        //{
        //    await OnConfirm.InvokeAsync();
        //    MudDialog.Close();
        //}
    }
}
