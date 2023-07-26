namespace AccountManagementMicroservice.DTOs
{
    public class TopUpOperationDto
    {
        public String AccountName { get; set; }

        public Decimal TopUpAmount { get; set; }
    }
}