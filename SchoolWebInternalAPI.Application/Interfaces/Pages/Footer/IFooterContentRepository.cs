using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IFooterContentRepository
    {
        Task<FooterContent?> GetAsync(CancellationToken cancellationToken = default);
        Task<FooterContent> UpsertAsync(FooterContent entity, CancellationToken cancellationToken = default);
    }
}
