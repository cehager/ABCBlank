using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking
{
    public partial class UpdateAccountHolderDialog
    {
        [Parameter]
        public UpdateAccountHolder UpdateAccountHolderRequest { get; set; } = new UpdateAccountHolder();

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        MudForm _form = default!;

        void Cancel() => MudDialog.Cancel();

        private async Task SaveAsync()
        {
            var response = await _accountHolderService.UpdateAccountHolderAsync(UpdateAccountHolderRequest);
            if (response.IsSuccess)
            {
                _snackbar.Add("Account Holder updated successfully.", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
        }
    }
}
