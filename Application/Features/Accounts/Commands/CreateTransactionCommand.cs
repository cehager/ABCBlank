using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Common.Enums;
using Domain;
using MediatR;
using System;
using Common.Responses;

namespace Application.Features.Accounts.Commands
{
    public class CreateTransactionCommand : IRequest<ResponseWrapper<int>>
    {
        public TransactionRequest Transaction { get; set; }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ResponseWrapper<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public CreateTransactionCommandHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWrapper<int>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var accountInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Transaction.AccountId);
            if (accountInDb is not null)
            {
                if (request.Transaction.Type == TransactionType.Withdrawal)
                {
                    if (request.Transaction.Amount > accountInDb.Balance)
                        {
                        return new ResponseWrapper<int>().Failed(message: "Account does not have enough money available.");
                    }

                    var transaction = new Domain.Transaction()
                    {
                        AccountId = accountInDb.Id,
                        Amount = request.Transaction.Amount,
                        Type = TransactionType.Withdrawal,
                        Date = DateTime.Now
                    };

                    accountInDb.Balance -= request.Transaction.Amount;
                    await _unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transaction);
                    await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return new ResponseWrapper<int>().Success(data: transaction.Id, message: "Withdrawal saved.");
                }
                else if (request.Transaction.Type == TransactionType.Deposit) 
                {
                    var transaction = new Domain.Transaction()
                    {
                        AccountId = accountInDb.Id,
                        Amount = request.Transaction.Amount,
                        Type = TransactionType.Deposit,
                        Date = DateTime.Now
                    };

                    accountInDb.Balance += request.Transaction.Amount;
                    await _unitOfWork.WriteRepositoryFor<Transaction>().AddAsync(transaction);
                    await _unitOfWork.WriteRepositoryFor<Account>().UpdateAsync(accountInDb);
                    await _unitOfWork.CommitAsync(cancellationToken);
                    return new ResponseWrapper<int>().Success(data: transaction.Id, message: "Deposit saved.");
                }
               
            }
            return new ResponseWrapper<int>().Failed(message: "Account does not exist.");
        }
    }
 }

