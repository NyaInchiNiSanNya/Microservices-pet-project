using ShareModel.Requests;

namespace Orchestrator.IService
{
    public interface IBankInformationMicroserviceMessaging
    {
        public Task SendWithdrawalOperationMessage(WithdrawalOperationRequest withdrawalOperationRequestModel);
        public Task<> SendReplenishmentOperationMessage(ReplenishmentOperationRequest replenishmentOperationRequestModel);
    }
}