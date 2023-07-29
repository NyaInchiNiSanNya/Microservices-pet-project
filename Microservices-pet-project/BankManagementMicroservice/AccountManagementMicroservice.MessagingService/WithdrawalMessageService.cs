using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.SharedModels.Response;
using WithdrawalOperationRequest = AccountManagementMicroservice.SharedModels.Requests.WithdrawalOperationRequest;
using MassTransit;

namespace AccountManagementMicroservice.MessagingService
{

    public class WithdrawalMessageService :
        IConsumer<WithdrawalOperationRequest>
    {

        public async Task Consume(ConsumeContext<WithdrawalOperationRequest> context)
        {
            var operation = context.Message;

            var withdrawalOperationDto = new WithdrawalOperationDto()
            {
                WithdrawalAmount = operation.Amount,
                AccountName = operation.AccountName
            };

            //await _withdrawOperation.WithdrawMoneyFromAccount(withdrawalOperationDto);

            var result = new WithdrawalOperationResponse
            {
                AccountBalance = 129/*await _accountInformationService.GetAccountBalance(withdrawalOperationDto.AccountName)*/
            };

            
            Console.WriteLine(operation.AccountName);
        }
    }
}