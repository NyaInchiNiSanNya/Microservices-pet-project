using AccountInformationMicroservice.API.RequestModels;
using FluentValidation;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace AccountInformationMicroservice.API.FluentValidation
{
    public class GetAccountBalanceRequestValidator: AbstractValidator<GetAccountBalanceRequest>
    {
        public GetAccountBalanceRequestValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);

        }
    }
}
