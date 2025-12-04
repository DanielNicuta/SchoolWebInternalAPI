using System;

namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class HistoryPageContent : BaseEntity
    {
        public string Title { get; set; } = "Istoricul școlii";
        public string? Subtitle { get; set; }
        public string? ContentHtml { get; set; }        // long rich text
        public string? SideImageUrl { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
