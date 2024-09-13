using Application.Features.Accounts.Commands;
using Application.Features.Accounts.Quries;
using Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : BaseApiController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddAccountAsync([FromBody] CreateAccountRequest createAccount)
        {
            var response =
                await Sender.Send(new CreateAccountCommand() { RequestRecord = createAccount });

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("transact")]
        public async Task<IActionResult> TransactAsync([FromBody] TransactionRequest transaction)
        {
            var response = await Sender.Send(new CreateTransactionCommand() { Transaction = transaction });

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetAccountByIdAsync(int id)
        {
            var response = await Sender.Send(new GetAccountByIdQuery() { Id = id });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("by-account-number/{accountNumber}")]
        public async Task<IActionResult> GetAccountByAccountNumberAsync(string accountNumber)
        {
            var response = await Sender.Send(new GetAccountByAccountNumberQuery() { AccountNumber = accountNumber });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAccountsAsync()
        {
            var response = await Sender.Send(new GetAccountsQuery());
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("transactions/{accountId}")]
        public async Task<IActionResult> GetAccountTransactionsAsync(int accountId)
        {
            var response =
                await Sender.Send(new GetAccountTransactionsQuery() { AccountId = accountId });

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
