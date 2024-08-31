using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AccountHolder : Person
    {
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
