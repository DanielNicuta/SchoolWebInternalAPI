using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class FooterContentRepository : IFooterContentRepository
    {
        private readonly SchoolDbContext _context;

        public FooterContentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<FooterContent?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.FooterContents
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<FooterContent> UpsertAsync(FooterContent entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.FooterContents.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.FooterContents.Add(entity);
            }
            else
            {
                existing.FooterText = entity.FooterText;
                existing.UsefulLinksJson = entity.UsefulLinksJson;
                existing.SocialLinksJson = entity.SocialLinksJson;
                existing.NewsletterTitle = entity.NewsletterTitle;
                existing.NewsletterText = entity.NewsletterText;

                existing.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return existing ?? entity;
        }
    }
}
