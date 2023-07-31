using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagementMicroservice.CQRS.Commands;
using AccountManagementMicroservice.CQRS.Queries;
using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using MediatR;

namespace AccountManagementMicroservice.BusinessLogic
{
    public class AccountInformationService : IAccountInformationService
    {
        private readonly IMediator _mediator;

        public AccountInformationService(IMediator mediator)
        {
            _mediator = mediator ??
                        throw new NullReferenceException(nameof(mediator));
        }

        public async Task<Decimal> GetAccountBalance(String accountName)
        {
            return 10;
            //return await _mediator.Send(new GetAccountBalanceQuery()
            //{
            //    accountName = accountName
            //});
        }
    }
}
