using System.Globalization;
using ShareModel.Requests;

namespace Orchestrator.IService
{
    public interface IAccountInformationMicroserviceMessaging
    {
        public Task SendNewBalanceMessage(UpdateAccountBalanceRequest request);
        public Task<Decimal> GetBalanceMessage(GetAccountBalanceRequest request);
    }
}