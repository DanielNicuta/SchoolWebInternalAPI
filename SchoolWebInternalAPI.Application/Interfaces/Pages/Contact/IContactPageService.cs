using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IContactPageService
    {
        Task<ApiResponse<ContactPageResponseDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<ContactPageResponseDto>> UpdateAsync(
            ContactPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
