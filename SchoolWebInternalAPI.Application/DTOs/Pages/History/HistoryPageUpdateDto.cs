namespace SchoolWebInternalAPI.Application.DTOs.Pages.History;

public class HistoryPageUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? ContentHtml { get; set; }
    public string? SideImageUrl { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? OgImageUrl { get; set; }
}
