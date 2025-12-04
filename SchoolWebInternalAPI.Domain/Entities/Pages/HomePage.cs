namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class HomePage : BaseEntity
    {
        // Hero section
        public string HeroTitle { get; set; } = string.Empty;
        public string? HeroSubtitle { get; set; }
        public string? HeroButtonText { get; set; }
        public string? HeroButtonUrl { get; set; }
        public string? HeroImageUrl { get; set; }

        // About section
        public string? AboutTitle { get; set; }
        public string? AboutSubtitle { get; set; }
        public string? AboutHtml { get; set; }
        public string? AboutImageUrl { get; set; }

        // Highlight boxes
        public string? Highlight1Title { get; set; }
        public string? Highlight1Text { get; set; }
        public string? Highlight1Icon { get; set; }

        public string? Highlight2Title { get; set; }
        public string? Highlight2Text { get; set; }
        public string? Highlight2Icon { get; set; }

        public string? Highlight3Title { get; set; }
        public string? Highlight3Text { get; set; }
        public string? Highlight3Icon { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }

        // Metadata
        public DateTime LastUpdatedAt { get; set; }
        public string? LastUpdatedBy { get; set; }
        public bool IsPublished { get; set; }
    }
}
