using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Quries
{
    public class GetAccountTransactionsQuery : IRequest<ResponseWrapper<List<TransactionResponse>>>
    {
        public int AccountId { get; set; }
    }

    public class GetAccountTransactionsQueryHandler :
        IRequestHandler<GetAccountTransactionsQuery, ResponseWrapper<List<TransactionResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAccountTransactionsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWrapper<List<TransactionResponse>>> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactionsInDb = _unitOfWork.ReadRepositoryFor<Transaction>()
                .Entities
                .Where(Transaction =>  Transaction.AccountId == request.AccountId)
                .ToList();

            if (transactionsInDb.Count > 0)
            {
                return await Task.FromResult(new ResponseWrapper<List<TransactionResponse>>().Success(data: transactionsInDb.Adapt<List<TransactionResponse>>()));
            }

            return await Task.FromResult(new ResponseWrapper<List<TransactionResponse>>().Failed(message: "No transactions were found for this account."));

        }
    }
}
