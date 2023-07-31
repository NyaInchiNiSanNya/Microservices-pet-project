using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using AutoMapper;
using FluentValidation;
using MassTransit;
using MassTransit.Transports;
using ShareModel.Requests;
using ShareModel.Response;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AccountManagementMicroservice.MessagingService
{

    public class ReplenishmentMessageService :
        IConsumer<ReplenishmentOperationRequest>
    {
        private readonly IValidator<ReplenishmentOperationRequest> _operationValidator;
        private readonly ITopUpOperationService _replenishmentOperation;
        private readonly IMapper _mapper;
        private readonly IAccountInformationService _accountInformationService;

        public ReplenishmentMessageService(IValidator<ReplenishmentOperationRequest> operationValidator
            , ITopUpOperationService withdrawOperation
            , IMapper mapper
            , IAccountInformationService accountInformationService)
        {
            _operationValidator = operationValidator ??
                                  throw new NullReferenceException(nameof(operationValidator));
            _replenishmentOperation = withdrawOperation ??
                                      throw new NullReferenceException(nameof(withdrawOperation));
            _mapper = mapper ??
                      throw new NullReferenceException(nameof(mapper));

            _accountInformationService = accountInformationService ??
                                         throw new NullReferenceException(nameof(accountInformationService));
        }

        public async Task Consume(ConsumeContext<ReplenishmentOperationRequest> context)
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

                await _replenishmentOperation.TopUpMoneyToAccount(_mapper.Map<TopUpOperationDto>(operation));

                var balance = await _accountInformationService.GetAccountBalance(operation.AccountName);

                await context.RespondAsync(new ReplenishmentOperationResponse
                {
                    AccountName = operation.AccountName,
                    AccountBalance = balance
                });
            }
            
            catch (NullReferenceException)
            {
                await context.RespondAsync(new ReplenishmentOperationResponse
                {
                    AccountName = String.Empty,
                    AccountBalance = 0
                });
            }

        }
    }
}