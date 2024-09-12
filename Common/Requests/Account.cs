using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Requests
{
    public record CreateAccountRequest(int AccountHolderId, decimal Balance, AccountType Type); //this is a DTO

    //public record WithdrawalRequest(int AccountId, decimal Amount);
    //public record DepositRequest(int AccountId, decimal Amount);
    public record TransactionRequest(int AccountId, decimal Amount, TransactionType Type);
}
