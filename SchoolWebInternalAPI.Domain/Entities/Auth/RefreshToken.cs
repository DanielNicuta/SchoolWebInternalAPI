namespace SchoolWebInternalAPI.Domain.Entities.Auth
{
    public class RefreshToken
    {
        public int Id { get; set; }

        // Identity user Id is string (GUID-like)
        public string UserId { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        // Rotation support
        public string? ReplacedByToken { get; set; }

        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
    }
}
