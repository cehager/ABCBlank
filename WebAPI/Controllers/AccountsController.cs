using Application.Features.Accounts.Commands;
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
                await Sender.Send(new CreateAccountCommand() { CreateAccount = createAccount });

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}
