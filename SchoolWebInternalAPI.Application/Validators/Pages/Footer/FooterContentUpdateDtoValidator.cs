using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Footer
{
    public class FooterContentUpdateDtoValidator : AbstractValidator<FooterContentUpdateDto>
    {
        public FooterContentUpdateDtoValidator()
        {
            RuleFor(x => x.FooterText)
                .MaximumLength(1000)
                .WithMessage("Footer text cannot exceed 1000 characters.");

            RuleFor(x => x.UsefulLinksJson)
                .MaximumLength(5000)
                .WithMessage("Useful links JSON is too long.")
                .Must(BeValidJson)
                .When(x => !string.IsNullOrWhiteSpace(x.UsefulLinksJson))
                .WithMessage("Useful links must contain valid JSON.");

            RuleFor(x => x.SocialLinksJson)
                .MaximumLength(5000)
                .WithMessage("Social links JSON is too long.")
                .Must(BeValidJson)
                .When(x => !string.IsNullOrWhiteSpace(x.SocialLinksJson))
                .WithMessage("Social links must contain valid JSON.");

            RuleFor(x => x.NewsletterTitle)
                .MaximumLength(150)
                .WithMessage("Newsletter title cannot exceed 150 characters.");

            RuleFor(x => x.NewsletterText)
                .MaximumLength(1000)
                .WithMessage("Newsletter text cannot exceed 1000 characters.");
        }

        private bool BeValidJson(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return true;

            try
            {
                System.Text.Json.JsonDocument.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
