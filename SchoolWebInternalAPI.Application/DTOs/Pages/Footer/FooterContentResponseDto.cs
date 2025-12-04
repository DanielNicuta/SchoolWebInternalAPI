namespace SchoolWebInternalAPI.Application.DTOs.Pages.Footer;

public class FooterContentResponseDto
{
    public string? FooterText { get; set; }
    public string? UsefulLinksJson { get; set; }
    public string? SocialLinksJson { get; set; }

    public string? NewsletterTitle { get; set; }
    public string? NewsletterText { get; set; }
}
