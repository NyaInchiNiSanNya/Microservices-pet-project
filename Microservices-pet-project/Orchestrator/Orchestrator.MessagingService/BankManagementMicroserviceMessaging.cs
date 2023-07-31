using MassTransit;
using Orchestrator.IService;
using ShareModel.Requests;
using ShareModel.Response;


namespace Orchestrator.MessagingService
{
    public class BankManagementMicroserviceMessaging : IBankManagementMicroserviceMessaging
    {
        IRequestClient<WithdrawalOperationRequest> _clientWithdrawal;
        IRequestClient<ReplenishmentOperationRequest> _clientReplenishment;

        public BankManagementMicroserviceMessaging(IRequestClient<WithdrawalOperationRequest> clientWithdrawal
            , IRequestClient<ReplenishmentOperationRequest> clientReplenishment)
        {
            _clientWithdrawal = clientWithdrawal;
            _clientReplenishment = clientReplenishment;
        }

        public async Task<Decimal> SendWithdrawalOperationMessage(WithdrawalOperationRequest withdrawalOperationRequestModel)
        {
            var response = await _clientWithdrawal.GetResponse<WithdrawalOperationResponse>(new
            {
                withdrawalOperationRequestModel.AccountName,
                withdrawalOperationRequestModel.Amount
            });


            if (String.IsNullOrEmpty(response.Message.AccountName))
            {
                throw new NullReferenceException(nameof(withdrawalOperationRequestModel));
            }
            if (response.Message.AccountBalance<0)
            {
                throw new InvalidOperationException(nameof(response));
            }

            return response.Message.AccountBalance;
        }

        public async Task<Decimal> SendReplenishmentOperationMessage(ReplenishmentOperationRequest replenishmentOperationRequestModel)
        {
            var response = await _clientReplenishment.GetResponse<ReplenishmentOperationResponse>(new
            {
                replenishmentOperationRequestModel.AccountName,
                replenishmentOperationRequestModel.Amount
            });


            if (String.IsNullOrEmpty(response.Message.AccountName))
            {
                throw new NullReferenceException(nameof(replenishmentOperationRequestModel));
            }

            return response.Message.AccountBalance;

        }
    }
}