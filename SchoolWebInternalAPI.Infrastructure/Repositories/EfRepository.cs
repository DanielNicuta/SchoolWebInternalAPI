using SchoolWebInternalAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolWebInternalAPI.Infrastructure.Repositories
{
    public class EfRepository<T> where T : class
    {
        protected readonly SchoolDbContext _db;

        public EfRepository(SchoolDbContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllAsync()
            => await _db.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _db.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity)
        {
            _db.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
