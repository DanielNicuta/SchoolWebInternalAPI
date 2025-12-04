using SchoolWebInternalAPI.Application.DTOs.Pages.Home;
using SchoolWebInternalAPI.Application.Common.Models;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IHomePageService
    {
        Task<ApiResponse<HomePageResponseDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<HomePageResponseDto>> UpdateAsync(HomePageUpdateDto dto, string? updatedBy = null, CancellationToken cancellationToken = default);
    }
}
