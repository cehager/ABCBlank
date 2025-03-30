using BankUI.Services;
using Common.Responses;
using Domain;
using MudBlazor;

namespace BankUI.Pages.Banking
{
    public partial class AccountHolderList
    {
        private List<AccountHolderResponse> AccountHolders { get; set; } = [];
        private bool _loading = true;
        protected override async Task OnInitializedAsync()
        {
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
    }
}
