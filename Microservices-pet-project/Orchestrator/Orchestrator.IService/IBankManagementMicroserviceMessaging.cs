using Orchestrator.SharedModels.Request;

namespace Orchestrator.IService
{
    public interface IBankManagementMicroserviceMessaging
    {
        public Task<Decimal> SendWithdrawalOperationMessage(WithdrawalOperationRequest withdrawalOperationRequestModel);
    }
}