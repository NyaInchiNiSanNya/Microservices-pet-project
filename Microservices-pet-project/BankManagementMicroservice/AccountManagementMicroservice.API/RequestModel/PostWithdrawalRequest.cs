namespace AccountManagementMicroservice.RequestModel
{
    public class PostWithdrawalRequest
    {
        public String AccountName { get; set; }

        public Decimal WithdrawalAmount { get; set; }
    }
}
