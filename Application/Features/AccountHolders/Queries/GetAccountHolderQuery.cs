using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Application.Features.AccountHolders.Queries
{
    public class GetAccountHoldersQuery : IRequest<ResponseWrapper<List<AccountHolderResponse>>>
    {

    }

    public class GetAccountHoldersQueryHandler : IRequestHandler<GetAccountHoldersQuery, ResponseWrapper<List<AccountHolderResponse>>>
    {

        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAccountHoldersQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWrapper<List<AccountHolderResponse>>> Handle(GetAccountHoldersQuery request, CancellationToken cancellationToken)
        {
            var accountHoldersInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetAllAsync();

            return new ResponseWrapper<List<AccountHolderResponse>>().Success(accountHoldersInDb.Adapt<List<AccountHolderResponse>>());
        }
    }
}
