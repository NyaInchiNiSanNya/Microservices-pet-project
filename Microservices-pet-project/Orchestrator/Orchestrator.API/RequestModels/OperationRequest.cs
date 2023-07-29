namespace Orchestrator.API.RequestModels
{
    public class OperationRequest
    {
        public String AccountName { get; set; }
        public Decimal Amount { get; set; }
        public Boolean Withdrawal { get; set; }
        public Boolean Replenishment { get; set; }
    }
}
