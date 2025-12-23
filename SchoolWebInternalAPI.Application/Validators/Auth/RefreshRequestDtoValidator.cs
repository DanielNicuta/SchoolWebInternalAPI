using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Auth;

namespace SchoolWebInternalAPI.Application.Validators.Auth;

public class RefreshRequestDtoValidator : AbstractValidator<AuthResponseDto>
{
    public RefreshRequestDtoValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken is required.")
            .MaximumLength(500);
    }
}
