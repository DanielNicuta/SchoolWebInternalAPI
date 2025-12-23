using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Teachers
                .AsNoTracking()
                .OrderBy(t => t.LastName)
                .ThenBy(t => t.FirstName)
                .ToListAsync(cancellationToken);
        }

        public async Task<Teacher?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<Teacher> AddAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return teacher;
        }

        public async Task<Teacher?> UpdateAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            var existing = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == teacher.Id, cancellationToken);

            if (existing == null)
            {
                return null;
            }

            _context.Entry(existing).CurrentValues.SetValues(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return existing;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (teacher == null)
            {
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
