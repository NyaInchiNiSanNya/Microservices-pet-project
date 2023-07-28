using AccountInformationMicroservice.CQRS.Commands;
using AccountInformationMicroservice.CQRS.Queries;
using AccountInformationMicroservice.DTOs;
using AccountInformationMicroservice.IServices;
using MediatR;

namespace AccountInformationMicroservice.Services
{
    public class AccountInformationManagmentService : IAccountInformationManagmentService
    {
        private readonly IMediator _mediator;

        public AccountInformationManagmentService(IMediator mediator)
        {
            _mediator = mediator
                        ?? throw new NullReferenceException(
                            nameof(mediator));
        }


        public async Task<Decimal> GetAccountBalanceByAccountNameAsync(String accountName)
        {
            if (String.IsNullOrEmpty(accountName))
            {
                throw new ArgumentNullException(nameof(accountName));
            }


            return await _mediator.Send(new GetAccountBalanceQuery()
            {
                AccountName = accountName
            });
        }


        public async Task SetNewAccountBalanceAsync(AccountInformationDto newAccountBalanceInformationDto)
        {
            await _mediator.Send(new PutNewAccountBalanceCommand()
            {
                AccountName = newAccountBalanceInformationDto.AccountName,
                NewBalance = newAccountBalanceInformationDto.AccountBalance
            });
        }
    }
}