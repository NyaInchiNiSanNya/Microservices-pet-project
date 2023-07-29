namespace Orchestrator.SharedModels.Request
{
    public class WithdrawalOperationRequest
    {
        public String AccountName { get; set; }
        public Decimal Amount { get; set; }
    }
}