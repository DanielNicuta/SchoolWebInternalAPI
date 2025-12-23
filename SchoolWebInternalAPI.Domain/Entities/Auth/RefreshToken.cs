using System.ComponentModel.DataAnnotations;

namespace SchoolWebInternalAPI.Domain.Entities.Auth;

public class RefreshToken
{
    public int Id { get; set; }

    [Required]
    [MaxLength(256)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [MaxLength(512)]
    public string Token { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }

    public bool IsActive => RevokedAt == null && ExpiresAt > DateTime.UtcNow;
}
