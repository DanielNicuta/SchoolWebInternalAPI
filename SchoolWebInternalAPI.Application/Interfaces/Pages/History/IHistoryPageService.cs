using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IHistoryPageService
    {
        Task<ApiResponse<HistoryPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<HistoryPageUpdateDto>> UpdateAsync(
            HistoryPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
