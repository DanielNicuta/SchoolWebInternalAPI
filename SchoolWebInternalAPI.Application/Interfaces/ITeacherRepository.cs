namespace SchoolWebInternalAPI.Application.Interfaces;

using SchoolWebInternalAPI.Domain.Entities;

public interface ITeacherRepository
{
    Task<IEnumerable<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(Guid id);
    Task AddAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher);
    Task DeleteAsync(Guid id);
}
