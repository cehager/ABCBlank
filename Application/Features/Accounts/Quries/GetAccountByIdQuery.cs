using Application.Repositories;
using Common.Responses;
using Common.Wrapper;
using Domain;
using MediatR;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Quries
{
    public class GetAccountByIdQuery : IRequest<ResponseWrapper<AccountResponse>>
    {
        public int Id { get; set; }
    }

    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, ResponseWrapper<AccountResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAccountByIdQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseWrapper<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var accountInDb = await _unitOfWork.ReadRepositoryFor<Account>().GetByIdAsync(request.Id);

            if (accountInDb is not null)
            {
                return new ResponseWrapper<AccountResponse>().Success(data: accountInDb.Adapt<AccountResponse>());
            }

            return new ResponseWrapper<AccountResponse>().Failed("Account does not exist.");
        }
    }
}
