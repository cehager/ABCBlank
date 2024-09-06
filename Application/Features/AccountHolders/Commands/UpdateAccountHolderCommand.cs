using Application.Repositories;
using Common.Requests;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountHolders.Commands
{
    public class UpdateAccountHolderCommand : IRequest<ResponseWrapper<int>>
    {
        public UpdateAccountHolder UpdateAccountHolder { get; set; }

        public class UpdateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
                 : IRequestHandler<UpdateAccountHolderCommand, ResponseWrapper<int>>
        {
            private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

            public async Task<ResponseWrapper<int>> Handle(UpdateAccountHolderCommand request, CancellationToken cancellationToken)
            {
                var accountHolderInDb = 
                    await _unitOfWork.ReadRepositoryFor<AccountHolder>().GetByIdAsync(request.UpdateAccountHolder.Id);


                if (accountHolderInDb is not null)
                {
                    var updatedAccountHolder = accountHolderInDb.Update(request.UpdateAccountHolder.FirstName,
                        request.UpdateAccountHolder.LastName, request.UpdateAccountHolder.Email, request.UpdateAccountHolder.ContactNumber);

                    await _unitOfWork.WriteRepositoryFor<AccountHolder>().UpdateAsync(updatedAccountHolder);
                    await _unitOfWork.CommitAsync(cancellationToken);

                    return new ResponseWrapper<int>().Success(updatedAccountHolder.Id, "Account Holder updated successfully.");
                }

                return new ResponseWrapper<int>().Failed("Account Holder does not exit.");



            }
        }

    }
}
