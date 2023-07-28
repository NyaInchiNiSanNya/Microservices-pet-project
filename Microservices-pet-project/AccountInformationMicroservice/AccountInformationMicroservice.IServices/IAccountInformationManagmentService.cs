using AccountInformationMicroservice.DTOs;

namespace AccountInformationMicroservice.IServices
{
    public interface IAccountInformationManagmentService
    {
        public Task<Decimal> GetAccountBalanceByAccountNameAsync(String accountName);
        public Task SetNewAccountBalanceAsync(AccountInformationDto newAccountBalanceDto);
    }
}