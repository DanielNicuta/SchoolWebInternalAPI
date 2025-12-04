using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class HistoryPageRepository : IHistoryPageRepository
    {
        private readonly SchoolDbContext _context;

        public HistoryPageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<HistoryPage?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.HistoryPages
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<HistoryPage> UpsertAsync(HistoryPage entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.HistoryPages.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.HistoryPages.Add(entity);
            }
            else
            {
                existing.Title = entity.Title;
                existing.Subtitle = entity.Subtitle;
                existing.ContentHtml = entity.ContentHtml;
                existing.SideImageUrl = entity.SideImageUrl;

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
