using System;

namespace SchoolWebInternalAPI.Domain.Entities.PagesCSM
{
    public class LinksPageContent : BaseEntity
    {
        public string Title { get; set; } = "Link-uri utile";
        public string? IntroHtml { get; set; }

        // For v1 we can store the whole list as HTML or JSON;
        // later we can normalize into a separate Link entity.
        public string? LinksHtml { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
