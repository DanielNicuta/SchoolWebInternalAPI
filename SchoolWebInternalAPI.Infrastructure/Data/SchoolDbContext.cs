using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Infrastructure.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; } = null!;
        
        // Add more DbSets here (Announcements, Pages, Documents, Gallery...)
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations go here if needed
        }
    }
}
