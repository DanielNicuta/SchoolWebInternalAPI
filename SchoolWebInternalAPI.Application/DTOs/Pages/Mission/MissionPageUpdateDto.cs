namespace SchoolWebInternalAPI.Application.DTOs.Pages.Mission;

public class MissionPageUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string? IntroHtml { get; set; }
    public string? MissionHtml { get; set; }
    public string? ImageUrl { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? OgImageUrl { get; set; }
}
