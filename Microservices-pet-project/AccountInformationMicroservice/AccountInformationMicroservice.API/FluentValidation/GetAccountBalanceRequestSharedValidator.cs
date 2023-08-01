using FluentValidation;
using ShareModel.Requests;

namespace AccountInformationMicroservice.API.FluentValidation
{
    public class GetAccountBalanceRequestSharedValidator : AbstractValidator<GetAccountBalanceRequest>
    {
        public GetAccountBalanceRequestSharedValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);

        }
    }
}
