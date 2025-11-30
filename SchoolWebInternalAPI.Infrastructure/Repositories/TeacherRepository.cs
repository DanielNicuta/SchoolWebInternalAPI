using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    // Fake in-memory for now
    private readonly List<Teacher> _teachers = new();

    public Task<IEnumerable<Teacher>> GetAllAsync() =>
        Task.FromResult(_teachers.AsEnumerable());

    public Task<Teacher?> GetByIdAsync(Guid id) =>
        Task.FromResult(_teachers.FirstOrDefault(t => t.Id == id));

    public Task AddAsync(Teacher teacher)
    {
        _teachers.Add(teacher);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Teacher teacher)
    {
        var existing = _teachers.FirstOrDefault(t => t.Id == teacher.Id);
        if (existing != null)
        {
            existing.Name = teacher.Name;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var teacher = _teachers.FirstOrDefault(t => t.Id == id);
        if (teacher != null)
            _teachers.Remove(teacher);

        return Task.CompletedTask;
    }
}
