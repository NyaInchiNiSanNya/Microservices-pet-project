namespace AccountManagementMicroservice.DTOs
{
    public class WithdrawalOperationDto
    {
        public String AccountName { get; set; }

        public Decimal WithdrawalAmount { get; set; }
    }
}