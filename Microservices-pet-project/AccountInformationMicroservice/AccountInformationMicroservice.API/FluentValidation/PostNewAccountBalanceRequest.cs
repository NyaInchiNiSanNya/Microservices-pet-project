using AccountInformationMicroservice.API.RequestModels;
using FluentValidation;

namespace AccountInformationMicroservice.API.FluentValidation
{
    public class PutNewAccountBalanceRequestValid : AbstractValidator<PutNewAccountBalanceRequest>
    {
        public PutNewAccountBalanceRequestValid()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);
            
            RuleFor(x => x.NewAccountBalance)
                .GreaterThanOrEqualTo(0);
        }
    }
}
