using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IFooterPageRepository
    {
        Task<FooterPage?> GetAsync(CancellationToken cancellationToken = default);
        Task<FooterPage> UpsertAsync(FooterPage entity, CancellationToken cancellationToken = default);
    }
}
