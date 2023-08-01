using FluentValidation;
using ShareModel.Requests;

namespace AccountInformationMicroservice.API.FluentValidation
{

    public class UpdateBalanceRequestSharedValidator : AbstractValidator<UpdateAccountBalanceRequest>
    {
        public UpdateBalanceRequestSharedValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);

            RuleFor(x => x.newBalance)
                .NotNull()
                .GreaterThan(0);

        }
    }
}
