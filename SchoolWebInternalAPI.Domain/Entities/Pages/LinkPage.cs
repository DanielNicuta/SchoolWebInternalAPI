namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class LinksPage : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? IntroHtml { get; set; }
        public string? LinksHtml { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
