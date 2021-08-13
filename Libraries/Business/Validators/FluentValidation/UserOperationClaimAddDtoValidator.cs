using Entities.Dtos;
using FluentValidation;

namespace Business.Validators.FluentValidation
{
    public class UserOperationClaimAddDtoValidator : AbstractValidator<UserOperationClaimAddDto>
    {
        public UserOperationClaimAddDtoValidator()
        {
            RuleFor(p => p.OperationClaimId).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
        }
    }
}
