using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface ILinksPageService
    {
        Task<ApiResponse<LinksPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default);

        Task<ApiResponse<LinksPageUpdateDto>> UpdateAsync(
            LinksPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
