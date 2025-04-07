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

namespace Application.Features.AccountHolders.Commands  //put this under a folder called Actions instead of features
{
    public class CreateAccountHolderCommand : IRequest<ResponseWrapper<int>>  //Use New instead of Create
    {
        public CreateAccountHolder CreateAccountHolder { get; set; } //this is the accountholder class / dto
     }

    //public class CreateAccountHolderCommandHandler(IUnitOfWork<int> unitOfWork)  //this is called by the webapi controller add route MediatR Send method
    //  : IRequestHandler<CreateAccountHolderCommand, ResponseWrapper<int>>       //IUnitOf Work is created by dependency injection from BuilderServicesExtensions
    //{
    //    private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
       
    //    public async Task<ResponseWrapper<int>> Handle(CreateAccountHolderCommand request, CancellationToken cancellationToken)
    //    {
    //        var accountHolder = request.CreateAccountHolder.Adapt<AccountHolder>();

    //        await _unitOfWork.WriteRepositoryFor<AccountHolder>().AddAsync(accountHolder);  
    //        //await _unitOfWork.CommitAsync(cancellationToken);

    //        return new ResponseWrapper<int>().Success(accountHolder.Id, "Account Holder created successfully.");
    //    }
    //}
}
