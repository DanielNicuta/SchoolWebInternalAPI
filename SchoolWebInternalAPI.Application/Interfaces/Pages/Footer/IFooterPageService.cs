using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IFooterContentService
    {
        Task<ApiResponse<FooterContentResponseDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<FooterContentResponseDto>> UpdateAsync(
            FooterContentUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
