using Application.Features.AccountHolders.Commands;
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

namespace Application.Features.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<ResponseWrapper<int>>
    {
        public CreateAccountRequest CreateAccount; //{ get; set; }  //dto
        //public CreateAccountRequest CreateAccount2;
    }

    public class CreateAccountCommandHandler(IUnitOfWork<int> unitOfWork)
     : IRequestHandler<CreateAccountCommand, ResponseWrapper<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork = unitOfWork;
        public async Task<ResponseWrapper<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            //Map incoming to Domain Account entity
            var account = request.CreateAccount.Adapt<Account>(); //data goes from CreateAccount (dto) in domain Account
            //int rv;
            //Generate an account number
            account.AccountNumber = AccountNumberGenerator.GenerateAccountNumber();

            //Activate the account
            account.IsActive = true;
            //Create/Add the account
            //account = 
            await _unitOfWork.WriteRepositoryFor<Account>().AddAsync(account);
            await _unitOfWork.CommitAsync(cancellationToken);
            //rv = await _unitOfWork.CommitAsync(cancellationToken);
            //if (rv != 0)
            //{
                //_unitOfWork.AccountingRespositoryFor<Account>().UpdateAccountingSytem(entity, codes, rv);
                //commit again
            //}
            return new ResponseWrapper<int>().Success(account.Id, "Account created successfully.");
        }
    }
}
