using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class LinksPageRepository : ILinksPageRepository
    {
        private readonly SchoolDbContext _context;

        public LinksPageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<LinksPage?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.LinksPages
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<LinksPage> UpsertAsync(LinksPage entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.LinksPages.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.LinksPages.Add(entity);
            }
            else
            {
                existing.Title = entity.Title;
                existing.IntroHtml = entity.IntroHtml;
                existing.LinksHtml = entity.LinksHtml;

                existing.SeoTitle = entity.SeoTitle;
                existing.SeoDescription = entity.SeoDescription;
                existing.OgImageUrl = entity.OgImageUrl;

                existing.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return existing ?? entity;
        }
    }
}
