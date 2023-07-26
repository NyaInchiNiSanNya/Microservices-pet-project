using AccountManagementMicroservice.DTOs;

namespace AccountManagementMicroservice.IServices
{
    public interface IWithdrawOperationService
    {
        public Task WithdrawMoneyFromAccount(WithdrawalOperationDto withdrawOperation);
    }
}