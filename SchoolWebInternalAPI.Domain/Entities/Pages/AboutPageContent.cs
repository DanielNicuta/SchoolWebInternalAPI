namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class AboutPageContent : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? MainHtml { get; set; }           // main content block (rich text / html)
        public string? SideImageUrl { get; set; }

        // Mission/Vision sections
        public string? MissionTitle { get; set; }
        public string? MissionText { get; set; }
        public string? VisionTitle { get; set; }
        public string? VisionText { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
