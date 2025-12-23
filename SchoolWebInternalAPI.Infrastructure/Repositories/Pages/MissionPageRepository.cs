using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class MissionPageRepository : IMissionPageRepository
    {
        private readonly SchoolDbContext _context;

        public MissionPageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<MissionPage?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.MissionPages
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<MissionPage> UpsertAsync(MissionPage entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.MissionPages.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.MissionPages.Add(entity);
            }
            else
            {
                existing.Title = entity.Title;
                existing.IntroHtml = entity.IntroHtml;
                existing.MissionHtml = entity.MissionHtml;
                existing.ImageUrl = entity.ImageUrl;

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
