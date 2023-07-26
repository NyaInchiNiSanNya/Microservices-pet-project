using AccountManagementMicroservice.CQRS.Commands;
using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementMicroservice.BusinessLogic
{
    public class TopUpOperationService : ITopUpOperationService
    {
        private readonly IMediator _mediator;
        
        public TopUpOperationService(IMediator mediator)
        {
            _mediator = mediator ??
                        throw new NullReferenceException(nameof(mediator));
        }

        public async Task TopUpMoneyToAccount(TopUpOperationDto topUpOperationDto)
        {
            await _mediator.Send(new TopUpOperationCommand()
            {
                summToTopUp = topUpOperationDto.TopUpAmount,
                accountName = topUpOperationDto.AccountName
            });
        }
    }
}
