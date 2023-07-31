using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using AutoMapper;
using FluentValidation;
using MassTransit;
using ShareModel.Requests;
using ShareModel.Response;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AccountManagementMicroservice.MessagingService
{

    public class WithdrawalMessageService :
        IConsumer<WithdrawalOperationRequest>
    {
        private readonly IValidator<WithdrawalOperationRequest> _operationValidator;
        private readonly IWithdrawOperationService _withdrawOperation;
        private readonly IMapper _mapper;
        private readonly IAccountInformationService _accountInformationService;

        public WithdrawalMessageService(IValidator<WithdrawalOperationRequest> operationValidator
            , IWithdrawOperationService withdrawOperation
            , IMapper mapper
            , IAccountInformationService accountInformationService)
        {
            _operationValidator = operationValidator ??
                                  throw new NullReferenceException(nameof(operationValidator));
            _withdrawOperation = withdrawOperation ??
                                 throw new NullReferenceException(nameof(withdrawOperation));
            _mapper = mapper ??
                      throw new NullReferenceException(nameof(mapper));

            _accountInformationService = accountInformationService ??
                                         throw new NullReferenceException(nameof(accountInformationService));
        }

        public async Task Consume(ConsumeContext<WithdrawalOperationRequest> context)
        {
            
            var operation = context.Message;

            try
            {
                ValidationResult result = await _operationValidator
                    .ValidateAsync(operation);

                if (!result.IsValid)
                {
                    throw new ArgumentException(nameof(operation));
                }

                await _withdrawOperation.WithdrawMoneyFromAccount(_mapper.Map<WithdrawalOperationDto>(operation));

                var balance = await _accountInformationService.GetAccountBalance(operation.AccountName);

                await context.RespondAsync(new WithdrawalOperationResponse
                {
                    AccountName = operation.AccountName,
                    AccountBalance = balance
                });
            }
            
            catch (NullReferenceException)
            {
                await context.RespondAsync(new WithdrawalOperationResponse
                {
                    AccountName = String.Empty,
                    AccountBalance = 0
                });
            }
            catch (InvalidOperationException)
            {
                await context.RespondAsync(new WithdrawalOperationResponse
                {
                    AccountName = String.Empty,
                    AccountBalance = -1
                });
            }

        }
    }
}