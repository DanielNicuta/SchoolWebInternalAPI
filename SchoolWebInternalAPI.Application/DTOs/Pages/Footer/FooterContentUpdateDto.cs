namespace SchoolWebInternalAPI.Application.DTOs.Pages.Footer;

public class FooterContentUpdateDto
{
    public string? FooterText { get; set; }
    public string? UsefulLinksJson { get; set; }
    public string? SocialLinksJson { get; set; }

    public string? NewsletterTitle { get; set; }
    public string? NewsletterText { get; set; }
}
