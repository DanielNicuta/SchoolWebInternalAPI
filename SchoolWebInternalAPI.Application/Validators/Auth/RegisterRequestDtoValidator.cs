using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Auth;

namespace SchoolWebInternalAPI.Application.Validators.Auth;

public class RegisterRequestDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterRequestDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(256);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(128);

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100);
    }
}
