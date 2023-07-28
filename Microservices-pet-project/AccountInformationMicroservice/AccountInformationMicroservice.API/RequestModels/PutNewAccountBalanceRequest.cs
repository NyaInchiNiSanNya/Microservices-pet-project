namespace AccountInformationMicroservice.API.RequestModels
{
    public class PutNewAccountBalanceRequest
    {
        public String AccountName { get; set; }
        public Decimal NewAccountBalance { get; set; }
    }
}
