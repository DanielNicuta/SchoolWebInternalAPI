namespace SchoolWebInternalAPI.Application.DTOs.Pages.Contact;

public class ContactPageResponseDto
{
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }

    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public string? MapEmbedUrl { get; set; }
    public string? InfoHtml { get; set; }

    public string? SeoTitle { get; set; }
    public string? SeoDescription { get; set; }
    public string? OgImageUrl { get; set; }
}
