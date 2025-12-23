using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Organization
{
    public class OrganizationPageUpdateDtoValidator : AbstractValidator<OrganizationPageUpdateDto>
    {
        public OrganizationPageUpdateDtoValidator()
        {
            // Title (required)
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            // Description HTML
            RuleFor(x => x.DescriptionHtml)
                .MaximumLength(10000)
                .When(x => !string.IsNullOrWhiteSpace(x.DescriptionHtml))
                .WithMessage("Description HTML cannot exceed 10,000 characters.");

            // Org Chart Image
            RuleFor(x => x.OrgChartImageUrl)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.OrgChartImageUrl))
                .WithMessage("Org chart image URL cannot exceed 255 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.OrgChartImageUrl))
                .WithMessage("Org chart image URL must be a valid absolute URL.");

            // Org Chart File (PDF, PNG, etc.)
            RuleFor(x => x.OrgChartFileUrl)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.OrgChartFileUrl))
                .WithMessage("Org chart file URL cannot exceed 255 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.OrgChartFileUrl))
                .WithMessage("Org chart file URL must be a valid absolute URL.");

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

            // Open Graph Image
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
