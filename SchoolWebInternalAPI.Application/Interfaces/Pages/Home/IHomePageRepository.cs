using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IHomePageRepository
    {
        Task<HomePage?> GetAsync(CancellationToken cancellationToken = default);
        Task<HomePage> UpsertAsync(HomePage page, CancellationToken cancellationToken = default);
    }
}
