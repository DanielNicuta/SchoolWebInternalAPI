using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using System.Linq.Expressions;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Settings
{
    public class SiteSettingsUpdateDtoValidator : AbstractValidator<SiteSettingsUpdateDto>
    {
        public SiteSettingsUpdateDtoValidator()
        {
            RuleFor(x => x.SiteName)
                .NotEmpty().WithMessage("Site name is required.")
                .MaximumLength(150);

            ValidateUrlField(x => x.LogoUrl, "Logo URL");
            ValidateUrlField(x => x.FaviconUrl, "Favicon URL");

            RuleFor(x => x.DefaultLanguage)
                .MaximumLength(10)
                .When(x => !string.IsNullOrWhiteSpace(x.DefaultLanguage));

            RuleFor(x => x.ContactEmail)
                .MaximumLength(255)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.ContactEmail));

            RuleFor(x => x.ContactPhone)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.ContactPhone));

            RuleFor(x => x.Address)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            ValidateUrlField(x => x.FacebookUrl, "Facebook URL");
            ValidateUrlField(x => x.YoutubeUrl, "YouTube URL");
            ValidateUrlField(x => x.TwitterUrl, "Twitter URL");

            RuleFor(x => x.CookieBannerText)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.CookieBannerText));
        }

        private void ValidateUrlField(Expression<Func<SiteSettingsUpdateDto, string?>> selector, string fieldName)
        {
            RuleFor(selector)
                .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(selector.Compile()(x)))
                .WithMessage($"{fieldName} cannot exceed 255 characters.")
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(selector.Compile()(x)))
                .WithMessage($"{fieldName} must be a valid absolute URL.");
        }

        private bool BeValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url)) return true;
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
