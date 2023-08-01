using AccountInformationMicroservice.IServices;
using AutoMapper;
using FluentValidation;
using MassTransit;
using ShareModel.Requests;
using ShareModel.Response;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace AccountInformationMicroservice.MessagingConsume
{
    public class GetBalanceMessageService :
        IConsumer<GetAccountBalanceRequest>
    {
        private readonly IValidator<GetAccountBalanceRequest> _requestValidator;
        private readonly IAccountInformationManagmentService _accountInformation;

        public GetBalanceMessageService(IValidator<GetAccountBalanceRequest> requestValidator
            , IAccountInformationManagmentService accountInformation
            , IMapper mapper)
        {
            _requestValidator = requestValidator ??
                                throw new NullReferenceException(nameof(requestValidator));
            _accountInformation = accountInformation ??
                                  throw new NullReferenceException(nameof(accountInformation));

        }

        public async Task Consume(ConsumeContext<GetAccountBalanceRequest> context)
        {

            var operation = context.Message;

            try
            {
                FluentValidation.Results.ValidationResult result = await _requestValidator
                    .ValidateAsync(operation);

                if (!result.IsValid)
                {
                    throw new ArgumentException(nameof(operation));
                }

                var balance = await _accountInformation.GetAccountBalanceByAccountNameAsync(operation.AccountName);

                await context.RespondAsync(new AccountInformationResponse
                {
                    AccountName = operation.AccountName,
                    AccountBalance = balance
                });
            }

            catch (NullReferenceException)
            {
                await context.RespondAsync(new AccountInformationResponse
                {
                    AccountName = String.Empty,
                    AccountBalance = 0
                });
            }

        }
    }
}