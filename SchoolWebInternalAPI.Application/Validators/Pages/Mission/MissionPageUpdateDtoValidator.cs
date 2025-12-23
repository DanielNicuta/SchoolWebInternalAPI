using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Mission
{
    public class MissionPageUpdateDtoValidator : AbstractValidator<MissionPageUpdateDto>
    {
        public MissionPageUpdateDtoValidator()
        {
            // Title (required)
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            // Intro section HTML
            RuleFor(x => x.IntroHtml)
                .MaximumLength(5000)
                .When(x => !string.IsNullOrWhiteSpace(x.IntroHtml))
                .WithMessage("Intro HTML cannot exceed 5000 characters.");

            // Mission body HTML
            RuleFor(x => x.MissionHtml)
                .MaximumLength(10000)
                .When(x => !string.IsNullOrWhiteSpace(x.MissionHtml))
                .WithMessage("Mission HTML cannot exceed 10,000 characters.");

            // Main Image
            RuleFor(x => x.ImageUrl)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl))
                .WithMessage("Image URL cannot exceed 255 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl))
                .WithMessage("Image URL must be a valid absolute URL.");

            // SEO Title
            RuleFor(x => x.SeoTitle)
                .MaximumLength(150)
                .When(x => !string.IsNullOrWhiteSpace(x.SeoTitle))
                .WithMessage("SEO title cannot exceed 150 characters.");

            // SEO Description
            RuleFor(x => x.SeoDescription)
                .MaximumLength(300)
                .When(x => !string.IsNullOrWhiteSpace(x.SeoDescription))
                .WithMessage("SEO description cannot exceed 300 characters.");

            // OG Image URL
            RuleFor(x => x.OgImageUrl)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.OgImageUrl))
                .WithMessage("OG Image URL cannot exceed 255 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.OgImageUrl))
                .WithMessage("OG Image URL must be a valid absolute URL.");
        }

        private bool BeValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return true;

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
