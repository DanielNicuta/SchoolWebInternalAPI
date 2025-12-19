using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Domain.Entities.PagesCSM;

namespace SchoolWebInternalAPI.Infrastructure.Data
{
    public class SchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; } = null!;
        public DbSet<HomePage> HomePages { get; set; } = null!;
        public DbSet<ContactPage> ContactPages { get; set; } = null!;
        public DbSet<HistoryPage> HistoryPages { get; set; } = null!;
        public DbSet<LinksPage> LinksPages { get; set; } = null!;
        public DbSet<MissionPage> MissionPages { get; set; } = null!;
        public DbSet<OrganizationPage> OrganizationPages { get; set; } = null!;
        public DbSet<SiteSettings> SiteSettings { get; set; } = null!;
        public DbSet<FooterContent> FooterContents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This will configure Identity tables
            base.OnModelCreating(modelBuilder);

            // Your entities’ configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }
}
