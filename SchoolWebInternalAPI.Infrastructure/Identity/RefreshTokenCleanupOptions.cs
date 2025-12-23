namespace SchoolWebInternalAPI.Infrastructure.Identity;

public sealed class RefreshTokenCleanupOptions
{
    public int IntervalMinutes { get; set; } = 60;
    public int DeleteRevokedAfterDays { get; set; } = 7;
}
