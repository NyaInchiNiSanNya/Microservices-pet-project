namespace AccountManagementMicroservice.RequestModel
{
    public class PostTopUpRequest
    {
        public String AccountName { get; set; }

        public Decimal TopUpAmount { get; set; }
    }
}
