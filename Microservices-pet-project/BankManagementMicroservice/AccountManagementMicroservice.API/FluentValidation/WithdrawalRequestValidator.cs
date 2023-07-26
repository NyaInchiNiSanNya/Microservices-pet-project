using AccountManagementMicroservice.RequestModel;
using FluentValidation;

namespace AccountManagementMicroservice.FluentValidation
{
    public class WithdrawalRequestValidator : AbstractValidator<PostWithdrawalRequest>
    {
        public WithdrawalRequestValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);
            
            RuleFor(x => x.WithdrawalAmount)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000);
        }
    }
}
