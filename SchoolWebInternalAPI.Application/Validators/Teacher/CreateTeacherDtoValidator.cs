using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Teachers;

namespace SchoolWebInternalAPI.Application.Validators.Teachers
{
    public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
    {
        public TeacherCreateDtoValidator()
        {
            RuleFor(t => t.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(t => t.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(t => t.Title)
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.")
                .When(t => !string.IsNullOrWhiteSpace(t.Title));

            RuleFor(t => t.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.")
                .When(t => !string.IsNullOrWhiteSpace(t.Bio));

            RuleFor(t => t.PhotoUrl)
                .MaximumLength(255).WithMessage("Photo URL cannot exceed 255 characters.")
                .When(t => !string.IsNullOrWhiteSpace(t.PhotoUrl));
        }
    }
}
