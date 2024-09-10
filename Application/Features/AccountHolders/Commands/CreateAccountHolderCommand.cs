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
    public class CreateAccountHolderCommand : IRequest<ResponseWrapper<int>>
    {
        public CreateAccountHolder CreateAccountHolder; // { get; set; }
     }

    public class CreateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)
      : IRequestHandler<CreateAccountHolderCommand, ResponseWrapper<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
        
     
        public async Task<ResponseWrapper<int>> Handle(CreateAccountHolderCommand request, CancellationToken cancellationToken)
        {
            var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();

            await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created successfully.");
        }
    }
}
