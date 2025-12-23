using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface ILinksPageRepository
    {
        Task<LinksPage?> GetAsync(CancellationToken cancellationToken = default);
        Task<LinksPage> UpsertAsync(LinksPage entity, CancellationToken cancellationToken = default);
    }
}
