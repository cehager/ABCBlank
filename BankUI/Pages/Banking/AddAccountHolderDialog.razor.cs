using BankUI.Pages.Banking.Validator;
using Common.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BankUI.Pages.Banking
{
    public partial class AddAccountHolderDialog
    {
        [Parameter] public CreateAccountHolder CreateAccountHolderRequest { get; set; } = new CreateAccountHolder();

        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        MudForm _form = default!;

        private CreateAccountHolderValidator _validator = new CreateAccountHolderValidator();

        private async Task SubmitAsync()
        {
            await _form.Validate();
            if (_form.IsValid)
            {
                await SaveAsync();
            }
        }
  

        private async Task SaveAsync()
        {
            var response = await _accountHolderService.AddAccountHolderAsync(CreateAccountHolderRequest);
            if (response.IsSuccess)
            {
                _snackbar.Add("Account Holder added successfully.", Severity.Success);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }


            //MudDialog.Close();
        }

        void Cancel() => MudDialog.Cancel();

    }
}
