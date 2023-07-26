using AccountManagementMicroservice.CQRS.Commands;
using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using MediatR;

namespace AccountManagementMicroservice.BusinessLogic
{
    public class WithdrawOperationService : IWithdrawOperationService
    {
        private readonly IMediator _mediator;
        public WithdrawOperationService(IMediator mediator)
        {
            _mediator = mediator ??
                        throw new NullReferenceException(nameof(mediator));
        }

        public async Task WithdrawMoneyFromAccount(WithdrawalOperationDto withdrawalOperationDto)
        {
            await _mediator.Send(new WithdrawalOperationCommand()
            {
                summToWithdrawal = withdrawalOperationDto.WithdrawalAmount,
                accountName = withdrawalOperationDto.AccountName
            });
        }
    }
}