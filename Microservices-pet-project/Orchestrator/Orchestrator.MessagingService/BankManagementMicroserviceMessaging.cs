using MassTransit;
using Orchestrator.IService;
using WithdrawalOperationRequest = Orchestrator.SharedModels.Request.WithdrawalOperationRequest;


namespace Orchestrator.MessagingService
{
    public class BankManagementMicroserviceMessaging : IBankManagementMicroserviceMessaging
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public BankManagementMicroserviceMessaging(IPublishEndpoint publishEndpoint)
        {
        _publishEndpoint= publishEndpoint;

        }

        public async Task<Decimal> SendWithdrawalOperationMessage(WithdrawalOperationRequest withdrawalOperationRequestModel)
        {
            await _publishEndpoint.Publish<WithdrawalOperationRequest>(new
            {
                withdrawalOperationRequestModel.AccountName,
                withdrawalOperationRequestModel.Amount
            });


            return 1000;
        }

    }

}