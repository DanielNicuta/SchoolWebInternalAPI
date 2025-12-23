using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class HomePageRepository : IHomePageRepository
    {
        private readonly SchoolDbContext _context;

        public HomePageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<HomePage?> GetAsync(CancellationToken cancellationToken = default)
        {
            // You only expect a single row
            return await _context.HomePages
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<HomePage> UpsertAsync(HomePage page, CancellationToken cancellationToken = default)
        {
            var existing = await _context.HomePages.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                page.LastUpdatedAt = DateTime.UtcNow;
                _context.HomePages.Add(page);
            }
            else
            {
                // Map fields manually to keep tracking
                existing.HeroTitle = page.HeroTitle;
                existing.HeroSubtitle = page.HeroSubtitle;
                existing.HeroButtonText = page.HeroButtonText;
                existing.HeroButtonUrl = page.HeroButtonUrl;
                existing.HeroImageUrl = page.HeroImageUrl;

                existing.AboutTitle = page.AboutTitle;
                existing.AboutSubtitle = page.AboutSubtitle;
                existing.AboutHtml = page.AboutHtml;
                existing.AboutImageUrl = page.AboutImageUrl;

                existing.Highlight1Title = page.Highlight1Title;
                existing.Highlight1Text = page.Highlight1Text;
                existing.Highlight1Icon = page.Highlight1Icon;

                existing.Highlight2Title = page.Highlight2Title;
                existing.Highlight2Text = page.Highlight2Text;
                existing.Highlight2Icon = page.Highlight2Icon;

                existing.Highlight3Title = page.Highlight3Title;
                existing.Highlight3Text = page.Highlight3Text;
                existing.Highlight3Icon = page.Highlight3Icon;

                existing.SeoTitle = page.SeoTitle;
                existing.SeoDescription = page.SeoDescription;
                existing.OgImageUrl = page.OgImageUrl;

                existing.LastUpdatedAt = DateTime.UtcNow;
                existing.LastUpdatedBy = page.LastUpdatedBy;
                existing.IsPublished = page.IsPublished;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return existing ?? page;
        }
    }
}
