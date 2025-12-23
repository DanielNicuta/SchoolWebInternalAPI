using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IHistoryPageRepository
    {
        Task<HistoryPage?> GetAsync(CancellationToken cancellationToken = default);
        Task<HistoryPage> UpsertAsync(HistoryPage entity, CancellationToken cancellationToken = default);
    }
}
