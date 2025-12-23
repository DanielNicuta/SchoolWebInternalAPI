using Microsoft.EntityFrameworkCore;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Infrastructure.Data;

namespace SchoolWebInternalAPI.Infrastructure.Repositories.Pages
{
    public class OrganizationPageRepository : IOrganizationPageRepository
    {
        private readonly SchoolDbContext _context;

        public OrganizationPageRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<OrganizationPage?> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OrganizationPages
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<OrganizationPage> UpsertAsync(OrganizationPage entity, CancellationToken cancellationToken = default)
        {
            var existing = await _context.OrganizationPages.FirstOrDefaultAsync(cancellationToken);

            if (existing == null)
            {
                _context.OrganizationPages.Add(entity);
            }
            else
            {
                existing.Title = entity.Title;
                existing.DescriptionHtml = entity.DescriptionHtml;
                existing.OrgChartImageUrl = entity.OrgChartImageUrl;
                existing.OrgChartFileUrl = entity.OrgChartFileUrl;

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
