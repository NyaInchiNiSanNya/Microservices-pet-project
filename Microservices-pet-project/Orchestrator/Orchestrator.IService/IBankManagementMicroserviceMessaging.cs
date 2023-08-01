using ShareModel.Requests;

namespace Orchestrator.IService
{
    public interface IBankManagementMicroserviceMessaging
    {
        public Task<Decimal> SendWithdrawalOperationMessage(WithdrawalOperationRequest withdrawalOperationRequestModel);
        public Task<Decimal> SendReplenishmentOperationMessage(ReplenishmentOperationRequest replenishmentOperationRequestModel);
    }
}