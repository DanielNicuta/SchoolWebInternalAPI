using FluentValidation;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;

namespace SchoolWebInternalAPI.Application.Validators.Pages.Contact
{
    public class ContactPageUpdateDtoValidator : AbstractValidator<ContactPageUpdateDto>
    {
        public ContactPageUpdateDtoValidator()
        {
            // Title is required
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Page title is required.")
                .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");

            RuleFor(x => x.Subtitle)
                .MaximumLength(250);

            RuleFor(x => x.Address)
                .MaximumLength(300);

            RuleFor(x => x.Phone)
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email address format.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .MaximumLength(150);

            RuleFor(x => x.MapEmbedUrl)
                .MaximumLength(1000)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.MapEmbedUrl))
                .WithMessage("Map embed URL must be a valid URL.");

            RuleFor(x => x.InfoHtml)
                .MaximumLength(5000)
                .WithMessage("HTML content cannot exceed 5000 characters.");

            // SEO validation
            RuleFor(x => x.SeoTitle)
                .MaximumLength(60);

            RuleFor(x => x.SeoDescription)
                .MaximumLength(160);

            RuleFor(x => x.OgImageUrl)
                .MaximumLength(500)
                .Must(BeValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.OgImageUrl))
                .WithMessage("OG image URL must be a valid URL.");
        }

        private bool BeValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
