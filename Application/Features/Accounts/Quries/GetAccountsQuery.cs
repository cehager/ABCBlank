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
    public class GetAccountsQuery : IRequest<ResponseWrapper<List<AccountResponse>>>
    {
    }

    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, ResponseWrapper<List<AccountResponse>>>
    {
        private IUnitOfWork<int> _unitOfWork;

        public GetAccountsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWrapper<List<AccountResponse>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
           var accountsInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetAllAsync();

            if (accountsInDb.Count > 0)
            {
                return new ResponseWrapper<List<AccountResponse>>().Success(accountsInDb.Adapt<List<AccountResponse>>());
            }
            return new ResponseWrapper<List<AccountResponse>>().Failed("No accounts were found.");
        }
    }
}
