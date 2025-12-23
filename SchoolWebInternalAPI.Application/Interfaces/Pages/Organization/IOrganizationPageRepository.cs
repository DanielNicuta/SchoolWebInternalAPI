using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IOrganizationPageRepository
    {
        Task<OrganizationPage?> GetAsync(CancellationToken cancellationToken = default);
        Task<OrganizationPage> UpsertAsync(OrganizationPage entity, CancellationToken cancellationToken = default);
    }
}
