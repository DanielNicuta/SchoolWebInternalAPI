namespace SchoolWebInternalAPI.Application.DTOs.Pages.TeachersPage;

public class TeachersPageResponseDto
{
    public string PageTitle { get; set; } = string.Empty;
    public string? PageSubtitle { get; set; }
    public string? IntroHtml { get; set; }
    public string? HeroImageUrl { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? OgImageUrl { get; set; }
}
