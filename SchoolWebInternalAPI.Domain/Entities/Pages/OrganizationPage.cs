namespace SchoolWebInternalAPI.Domain.Entities.Pages
{
    public class OrganizationPage : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? DescriptionHtml { get; set; }
        public string? OrgChartImageUrl { get; set; }
        public string? OrgChartFileUrl { get; set; }

        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
