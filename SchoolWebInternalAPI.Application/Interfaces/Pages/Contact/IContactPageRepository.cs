using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IContactPageRepository
    {
        Task<ContactPage?> GetAsync(CancellationToken cancellationToken = default);
        Task<ContactPage> UpsertAsync(ContactPage page, CancellationToken cancellationToken = default);
    }
}
