using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Home
{
    public class HomePageUpdateDtoValidator : AbstractValidator<HomePageUpdateDto>
    {
        public HomePageUpdateDtoValidator()
        {
            // -------------------------
            // HERO SECTION
            // -------------------------

            RuleFor(x => x.HeroTitle)
                .NotEmpty().WithMessage("Hero title is required.")
                .MaximumLength(200).WithMessage("Hero title cannot exceed 200 characters.");

            RuleFor(x => x.HeroSubtitle)
                .MaximumLength(300).WithMessage("Hero subtitle cannot exceed 300 characters.");

            RuleFor(x => x.HeroButtonText)
                .MaximumLength(100).WithMessage("Button text cannot exceed 100 characters.");

            RuleFor(x => x.HeroButtonUrl)
                .MaximumLength(500).WithMessage("Button URL cannot exceed 500 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.HeroButtonUrl))
                .WithMessage("Hero button URL must be a valid URL format.");

            RuleFor(x => x.HeroImageUrl)
                .MaximumLength(500)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.HeroImageUrl))
                .WithMessage("Hero image URL must be a valid URL.");

            // -------------------------
            // ABOUT SECTION
            // -------------------------

            RuleFor(x => x.AboutTitle)
                .MaximumLength(200);

            RuleFor(x => x.AboutSubtitle)
                .MaximumLength(300);

            RuleFor(x => x.AboutHtml)
                .MaximumLength(5000).WithMessage("About HTML content cannot exceed 5000 characters.");

            RuleFor(x => x.AboutImageUrl)
                .MaximumLength(500)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.AboutImageUrl))
                .WithMessage("About image URL must be valid.");

            // -------------------------
            // HIGHLIGHT BOXES
            // -------------------------

            ValidateHighlight(x => x.Highlight1Title, x => x.Highlight1Text, x => x.Highlight1Icon);
            ValidateHighlight(x => x.Highlight2Title, x => x.Highlight2Text, x => x.Highlight2Icon);
            ValidateHighlight(x => x.Highlight3Title, x => x.Highlight3Text, x => x.Highlight3Icon);

            // -------------------------
            // SEO
            // -------------------------

            RuleFor(x => x.SeoTitle)
                .MaximumLength(60);

            RuleFor(x => x.SeoDescription)
                .MaximumLength(160);

            RuleFor(x => x.OgImageUrl)
                .MaximumLength(500)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.OgImageUrl))
                .WithMessage("OG image URL must be valid.");
        }

        // Helper validation functions
        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        private void ValidateHighlight(
            System.Linq.Expressions.Expression<Func<HomePageUpdateDto, string?>> title,
            System.Linq.Expressions.Expression<Func<HomePageUpdateDto, string?>> text,
            System.Linq.Expressions.Expression<Func<HomePageUpdateDto, string?>> icon)
        {
            RuleFor(title).MaximumLength(150);
            RuleFor(text).MaximumLength(500);
            RuleFor(icon).MaximumLength(100);
        }
    }
}
