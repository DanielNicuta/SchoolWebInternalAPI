using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Teacher> AddAsync(Teacher teacher, CancellationToken cancellationToken = default);
        Task<Teacher?> UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
