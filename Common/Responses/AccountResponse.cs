using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses
{
    public class AccountResponse
    {
        public string AccountNumber { get; set; }
        public int AccountHolderId { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public AccountType Type { get; set; }
        public AccountHolderResponse AccountHolder { get; set; }
    }
}
