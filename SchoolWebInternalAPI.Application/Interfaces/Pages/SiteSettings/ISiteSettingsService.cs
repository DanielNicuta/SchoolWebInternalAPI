using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface ISiteSettingsService
    {
        Task<ApiResponse<SiteSettingsUpdateDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<SiteSettingsUpdateDto>> UpdateAsync(
            SiteSettingsUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
