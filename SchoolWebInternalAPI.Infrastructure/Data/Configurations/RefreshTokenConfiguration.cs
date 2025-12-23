using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolWebInternalAPI.Domain.Entities.Auth;

namespace SchoolWebInternalAPI.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(x => x.UserId).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Token).HasMaxLength(512).IsRequired();

        // Unique token (fast refresh lookup)
        builder.HasIndex(x => x.Token).IsUnique();

        // Fast “delete all user tokens”
        builder.HasIndex(x => x.UserId);

        // Fast cleanup for expirations
        builder.HasIndex(x => x.ExpiresAt);

        // Fast cleanup for revoked tokens
        builder.HasIndex(x => x.RevokedAt);

        // Optional: common cleanup filter patterns
        builder.HasIndex(x => new { x.UserId, x.ExpiresAt });
    }
}
