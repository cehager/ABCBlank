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
    public class GetAccountByAccountNumberQuery : IRequest<ResponseWrapper<AccountResponse>>
    {
        public string AccountNumber { get; set; }
    }

    public class GetAccountByAccountNumberQueryHandler : IRequestHandler<GetAccountByAccountNumberQuery, ResponseWrapper<AccountResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAccountByAccountNumberQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByAccountNumberQuery request, CancellationToken cancellationToken)
        {
            var accountInDb =  _unitOfWork.ReadRepositoryFor<Account>()
                .Entities 
                .Where(account =>  account.AccountNumber == request.AccountNumber)
                .FirstOrDefault();

            if (accountInDb is not null)
            {
                return await Task.FromResult(new ResponseWrapper<AccountResponse>().Success(data: accountInDb.Adapt<AccountResponse>()));
            }

            return await Task.FromResult(new ResponseWrapper<AccountResponse>().Failed("Account does not exist."));
  
        }
    }
}
