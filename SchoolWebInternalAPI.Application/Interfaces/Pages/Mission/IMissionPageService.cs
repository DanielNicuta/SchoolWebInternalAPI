using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IMissionPageService
    {
        Task<ApiResponse<MissionPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default);

        Task<ApiResponse<MissionPageUpdateDto>> UpdateAsync(
            MissionPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
