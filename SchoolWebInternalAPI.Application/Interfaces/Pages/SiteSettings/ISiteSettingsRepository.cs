using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface ISiteSettingsRepository
    {
        Task<SiteSettings?> GetAsync(CancellationToken cancellationToken = default);
        Task<SiteSettings> UpsertAsync(SiteSettings entity, CancellationToken cancellationToken = default);
    }
}
