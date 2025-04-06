using BankUI.Pages.Shared;
using BankUI.Services;
using Common.Requests;
using Common.Responses;
using Domain;
using MudBlazor;
using MudBlazor.Extensions;

namespace BankUI.Pages.Banking
{
    public partial class AccountHolderList
    {
        private List<AccountHolderResponse> AccountHolders { get; set; } = [];
        private bool _loading = true;
        private Color striped = Color.Dark;
        protected override async Task OnInitializedAsync()
        {
            await LoadAccountHoldersAsync();
        }

        private async Task LoadAccountHoldersAsync()
        {
            //_loading = true;
            var response = await _accountHolderService.GetAllAccountHoldersAsync();
            if (response.IsSuccess)
            {
                AccountHolders = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackbar.Add(message, Severity.Error);
                }
            }
            _loading = false;
        }

        private async Task AddAccountHolderAsync()
        {
            var parameters = new DialogParameters();
            var options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true, BackdropClick = false };

            var dialog = _dialogService.Show<AddAccountHolderDialog>("Add Account Holder", parameters, options);

            var result = await dialog.Result;
            
            if ( !result.Canceled)
            {
                await LoadAccountHoldersAsync();
                await Console.Out.WriteLineAsync("All went well!!");

            }


        }

        private async Task UpdateAccountHolderAsync(int accountHolderId)
        {
            var parameters = new DialogParameters();
            var accountHolder = AccountHolders.FirstOrDefault(x => x.Id == accountHolderId);
            parameters.Add(nameof(UpdateAccountHolderDialog.UpdateAccountHolderRequest), new UpdateAccountHolder
            {
                Id = accountHolder.Id,
                FirstName = accountHolder.FirstName,
                LastName = accountHolder.LastName,
                Email = accountHolder.Email,
                ContactNumber = accountHolder.ContactNumber
            });
        
            var options = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true, BackdropClick = false };
            var dialog = _dialogService.Show<UpdateAccountHolderDialog>("Update Account Holder", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await LoadAccountHoldersAsync();
                await Console.Out.WriteLineAsync("All went well!!");
            }
        }

        private async Task DeleteAsync(int accountHolderId, string firstName, string lastName)
        {
            string message = $"Are you sure you want to delete {firstName} {lastName}?";

            var parameters = new DialogParameters()
            {
               {nameof(Shared.DeleteConfirmationDialog.Message), message},
            };
            //parameters.Add(nameof(DeleteAsync.AccountHolderId), accountHolderId);
            var options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, CloseButton = true, BackdropClick = false };
            var dialog = _dialogService.Show<DeleteConfirmationDialog>("Delete Account Holder", parameters, options);
            
            var result = await dialog.Result;
            if (!result.Canceled)
            {
                //await LoadAccountHoldersAsync();
                var response = await _accountHolderService.DeleteAccountHolderAsync(accountHolderId);
                if (response.IsSuccess)
                {
                    await LoadAccountHoldersAsync();  //can delete from list without calling the server
                    _snackbar.Add("Account Holder deleted successfully.", Severity.Success);
                }
                else
                {
                    foreach (var msg in response.Messages)
                    {
                        _snackbar.Add(msg, Severity.Error);
                    }
                }
                await Console.Out.WriteLineAsync("All went well!!");
            }
        }
    }
}
