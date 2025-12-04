using System;

namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class TeachersPageContent : BaseEntity
    {
        public string PageTitle { get; set; } = "Corpul profesoral";
        public string? PageSubtitle { get; set; }
        public string? IntroHtml { get; set; }
        public string? HeroImageUrl { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
