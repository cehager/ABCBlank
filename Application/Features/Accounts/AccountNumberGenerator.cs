using MediatR.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts
{
    public static class AccountNumberGenerator
    {
        public static string GenerateAccountNumber() => DateTime.Now.ToString("yyMMddHHmmss");
    }
}
