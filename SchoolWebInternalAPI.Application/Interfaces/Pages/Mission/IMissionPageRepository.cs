using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IMissionPageRepository
    {
        Task<MissionPage?> GetAsync(CancellationToken cancellationToken = default);
        Task<MissionPage> UpsertAsync(MissionPage entity, CancellationToken cancellationToken = default);
    }
}
