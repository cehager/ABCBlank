using Common.Enums;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Account : BaseEntity<int>
    {
        public int AccountNumber { get; set; }
        public int AccountHolderId { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public AccountType  Type { get; set; }

        public AccountHolder AccountHolder { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
