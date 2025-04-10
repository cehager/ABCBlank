using Application.Features.AccountHolders.Commands;
using Application.Repositories;
using Common.Wrapper;
using Domain;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AccountHolders.AccountHoldersHandlers
{
    public class CreateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)  //this is called by the webapi controller add route MediatR Send method
      : IRequestHandler<CreateAccountHolderCommand, ResponseWrapper<int>>       //IUnitOf Work is created by dependency injection from BuilderServicesExtensions
    {
        private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;

        public async Task<ResponseWrapper<int>> Handle(CreateAccountHolderCommand request, CancellationToken cancellationToken)
        {
            //var type = typeof(CreateAccountHolderCommand).Name;
            //var typeah = typeof(AccountHolder).Name;

            var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();

            await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);
            //await _unitOfWork.CommitAsync(cancellationToken);

            return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created successfully.");
        }
    }
}
