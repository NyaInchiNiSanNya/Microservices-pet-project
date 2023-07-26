using AccountManagementMicroservice.RequestModel;
using FluentValidation;

namespace AccountManagementMicroservice.FluentValidation
{
    public class PostTopUpValidator : AbstractValidator<PostTopUpRequest>
    {
        public PostTopUpValidator()
        {
            RuleFor(x => x.AccountName)
                .NotNull()
                .Length(12);
        }
    }
}
