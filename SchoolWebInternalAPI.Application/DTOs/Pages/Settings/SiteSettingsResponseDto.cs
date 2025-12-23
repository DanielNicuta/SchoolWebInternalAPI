namespace SchoolWebInternalAPI.Application.DTOs.Pages.Settings;

public class SiteSettingsResponseDto
{
    public string SiteName { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string? FaviconUrl { get; set; }
    public string? DefaultLanguage { get; set; }

    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Address { get; set; }

    public string? FacebookUrl { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? TwitterUrl { get; set; }

    public string? CookieBannerText { get; set; }
}
