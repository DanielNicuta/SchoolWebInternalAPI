using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Identity;

public sealed class RefreshTokenCleanupHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RefreshTokenCleanupHostedService> _logger;
    private readonly RefreshTokenCleanupOptions _options;

    public RefreshTokenCleanupHostedService(
        IServiceScopeFactory scopeFactory,
        ILogger<RefreshTokenCleanupHostedService> logger,
        IOptions<RefreshTokenCleanupOptions> options)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = TimeSpan.FromMinutes(Math.Max(1, _options.IntervalMinutes));

        _logger.LogInformation("RefreshToken cleanup service started. Interval: {Interval}", interval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanupOnce(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RefreshToken cleanup failed.");
            }

            await Task.Delay(interval, stoppingToken);
        }
    }

    private async Task CleanupOnce(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

        var now = DateTime.UtcNow;
        var revokeCutoff = now.AddDays(-Math.Max(1, _options.DeleteRevokedAfterDays));

        // Delete:
        // 1) Expired tokens (ExpiresAt <= now)
        // 2) Revoked tokens older than cutoff (RevokedAt != null && RevokedAt <= cutoff)
        var tokens = db.RefreshTokens.Where(t =>
            t.ExpiresAt <= now ||
            (t.RevokedAt != null && t.RevokedAt <= revokeCutoff));

        var deleted = await tokens.ExecuteDeleteAsync(ct);

        if (deleted > 0)
            _logger.LogInformation("RefreshToken cleanup deleted {Count} tokens.", deleted);
    }
}
