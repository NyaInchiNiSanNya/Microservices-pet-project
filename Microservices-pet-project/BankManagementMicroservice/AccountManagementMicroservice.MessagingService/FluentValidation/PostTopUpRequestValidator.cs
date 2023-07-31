using FluentValidation;
using ShareModel.Requests;

namespace AccountManagementMicroservice.FluentValidation
{
    public class ReplenishmentOperationRequestValidator : AbstractValidator<ReplenishmentOperationRequest>
    {
        public ReplenishmentOperationRequestValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);
        }
    }
}
