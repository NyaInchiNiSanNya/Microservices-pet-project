using FluentValidation;
using ShareModel.Requests;

namespace AccountManagementMicroservice.FluentValidation
{
    public class WithdrawalOperationRequestValidator : AbstractValidator<WithdrawalOperationRequest>
    {
        public WithdrawalOperationRequestValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);
        }
    }
}
