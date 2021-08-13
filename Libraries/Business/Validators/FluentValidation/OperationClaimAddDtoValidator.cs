using Entities.Dtos;
using FluentValidation;

namespace Business.Validators.FluentValidation
{
    public class OperationClaimAddDtoValidator : AbstractValidator<OperationClaimAddDto>
    {
        public OperationClaimAddDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(100);

            RuleFor(p => p.IsDefault).NotNull();
        }
    }
}
