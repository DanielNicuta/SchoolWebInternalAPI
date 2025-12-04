using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Infrastructure.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; } = null!;
        // inside SchoolDbContext (or your DbContext implementation)
        public DbSet<HomePageContent> HomePageContents { get; set; }
        public DbSet<HistoryPageContent> HistoryPageContents { get; set; }
        public DbSet<MissionPageContent> MissionPageContents { get; set; }
        public DbSet<OrganizationPageContent> OrganizationPageContents { get; set; }
        public DbSet<TeachersPageContent> TeachersPageContents { get; set; }
        public DbSet<LinksPageContent> LinksPageContents { get; set; }
        public DbSet<ContactPageContent> ContactPageContents { get; set; }
        public DbSet<SiteSettings> SiteSettings { get; set; }
        public DbSet<FooterContent> FooterContents { get; set; }


        
        // Add more DbSets here (Announcements, Pages, Documents, Gallery...)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API configurations go here if needed
        }
    }
}
