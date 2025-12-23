using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Auth;

namespace SchoolWebInternalAPI.Application.Validators.Auth;

public class LogoutRequestDtoValidator : AbstractValidator<LogoutRequestDto>
{
    public LogoutRequestDtoValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}
