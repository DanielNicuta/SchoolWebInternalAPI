using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class FooterPageRepository : IFooterPageRepository
    {
        private readonly SchoolDbContext _context;

        public FooterPageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<FooterPage?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.FooterPage
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<FooterPage> UpsertAsync(FooterPage entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.FooterPage.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.FooterPage.Add(entity);
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
