namespace SchoolWebInternalAPI.Application.DTOs.Pages.Organization;

public class OrganizationPageUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string? DescriptionHtml { get; set; }
    public string? OrgChartImageUrl { get; set; }
    public string? OrgChartFileUrl { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? OgImageUrl { get; set; }
}
