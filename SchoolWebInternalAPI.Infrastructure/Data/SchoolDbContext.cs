using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Domain.Entities.Auth;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Domain.Entities.PagesCSM;
using SchoolWebInternalAPI.Infrastructure.Identity;

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
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RefreshToken>()
                .HasOne(x => x.ParentToken)
                .WithMany()
                .HasForeignKey(x => x.ParentTokenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(x => x.Token)
                .IsUnique();

            modelBuilder.Entity<RefreshToken>()
                .HasIndex(x => new { x.UserId, x.FamilyId });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
        }
    }
}
