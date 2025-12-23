using System;

namespace SchoolWebInternalAPI.Domain.Entities.PagesCSM
{
    public class OrganizationPageContent : BaseEntity
    {
        public string Title { get; set; } = "Organigrama";
        public string? DescriptionHtml { get; set; }

        // This could be an image or PDF of the organigram
        public string? OrgChartImageUrl { get; set; }
        public string? OrgChartFileUrl { get; set; }

        // SEO
        public string? SeoTitle { get; set; }
        public string? SeoDescription { get; set; }
        public string? OgImageUrl { get; set; }
    }
}
