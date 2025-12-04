using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Links
{
    public class LinksPageUpdateDtoValidator : AbstractValidator<LinksPageUpdateDto>
    {
        public LinksPageUpdateDtoValidator()
        {
            // Title (required)
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            // Intro HTML content
            RuleFor(x => x.IntroHtml)
                .MaximumLength(5000)
                .When(x => !string.IsNullOrWhiteSpace(x.IntroHtml))
                .WithMessage("Intro HTML cannot exceed 5000 characters.");

            // Links HTML content (the big table of resources)
            RuleFor(x => x.LinksHtml)
                .MaximumLength(10000)
                .When(x => !string.IsNullOrWhiteSpace(x.LinksHtml))
                .WithMessage("Links HTML cannot exceed 10,000 characters.");

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
                .WithMessage("OG image URL cannot exceed 255 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.OgImageUrl))
                .WithMessage("OG image URL must be a valid absolute URL.");
        }

        private bool BeValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return true;

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
