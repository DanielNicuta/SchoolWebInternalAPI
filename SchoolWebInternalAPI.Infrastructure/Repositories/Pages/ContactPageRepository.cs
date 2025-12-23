using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class ContactPageRepository : IContactPageRepository
    {
        private readonly SchoolDbContext _context;

        public ContactPageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<ContactPage?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.ContactPages
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ContactPage> UpsertAsync(ContactPage page, CancellationToken cancellationToken = default)
        {
            var existing = await _context.ContactPages.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.ContactPages.Add(page);
            }
            else
            {
                existing.Title = page.Title;
                existing.Subtitle = page.Subtitle;
                existing.Address = page.Address;
                existing.Phone = page.Phone;
                existing.Email = page.Email;
                existing.MapEmbedUrl = page.MapEmbedUrl;
                existing.InfoHtml = page.InfoHtml;
                existing.SeoTitle = page.SeoTitle;
                existing.SeoDescription = page.SeoDescription;
                existing.OgImageUrl = page.OgImageUrl;
                existing.UpdatedAt = page.UpdatedAt;
                existing.UpdatedBy = page.UpdatedBy;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return existing ?? page;
        }
    }
}
