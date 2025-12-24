using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class SiteSettingsRepository : ISiteSettingsRepository
    {
        private readonly SchoolDbContext _context;

        public SiteSettingsRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<SiteSettings?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SiteSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<SiteSettings> UpsertAsync(SiteSettings entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.SiteSettings.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.SiteSettings.Add(entity);
            }
            else
            {
                existing.SiteName = entity.SiteName;
                existing.LogoUrl = entity.LogoUrl;
                existing.FaviconUrl = entity.FaviconUrl;
                existing.DefaultLanguage = entity.DefaultLanguage;

                existing.ContactEmail = entity.ContactEmail;
                existing.ContactPhone = entity.ContactPhone;
                existing.Address = entity.Address;

                existing.FacebookUrl = entity.FacebookUrl;
                existing.YoutubeUrl = entity.YoutubeUrl;
                existing.TwitterUrl = entity.TwitterUrl;

                existing.CookieBannerText = entity.CookieBannerText;

                existing.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return existing ?? entity;
        }
    }
}
