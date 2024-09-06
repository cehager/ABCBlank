using Application.Repositories;
using Common.Wrapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountHolders.Commands
{
    public class DeleteAccountHolderCommand : IRequest<ResponseWrapper<int>>
    {
        public int Id { get; set; }

        public class DeleteAccountHolderCommandHandler : IRequestHandler<DeleteAccountHolderCommand, ResponseWrapper<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork;

            public DeleteAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseWrapper<int>> Handle(DeleteAccountHolderCommand request, CancellationToken cancellationToken)
            {
                var accountHolderInDb = await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.Id);
                if (accountHolderInDb is not null)
                {
                    await _unitOfWork.WriteRepositoryFor<AccountHolder>().DeleteAsync(accountHolderInDb);
                    await _unitOfWork.CommitAsync(cancellationToken);

                    return new ResponseWrapper<int>().Success(accountHolderInDb.Id, "Account Holder Deleted Successfully");
                }

                return new ResponseWrapper<int>().Failed("Account Holder does not exist.");

            }
        }
    }
}
