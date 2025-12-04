using System;

namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class MissionPageContent : BaseEntity
    {
        public string Title { get; set; } = "Misiunea școlii";
        public string? IntroHtml { get; set; }          // intro paragraph
        public string? MissionHtml { get; set; }        // main mission text
        public string? ImageUrl { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
