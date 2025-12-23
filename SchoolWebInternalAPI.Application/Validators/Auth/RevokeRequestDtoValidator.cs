using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Auth;

namespace SchoolWebInternalAPI.Application.Validators.Auth;

public class RevokeRequestDtoValidator : AbstractValidator<RevokeRequestDto>
{
    public RevokeRequestDtoValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken is required.")
            .MaximumLength(500);
    }
}
