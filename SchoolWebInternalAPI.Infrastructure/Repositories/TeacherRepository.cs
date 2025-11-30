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

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            return await _context.Teachers.FindAsync(id);
        }

        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<bool> UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return false;

            _context.Teachers.Remove(teacher);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
