using System;

namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class ContactPageContent : BaseEntity
    {
        public string Title { get; set; } = "Contact";
        public string? Subtitle { get; set; }

        // Contact info
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        // Map (iframe embed or image)
        public string? MapEmbedUrl { get; set; }

        // Extra text
        public string? InfoHtml { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
