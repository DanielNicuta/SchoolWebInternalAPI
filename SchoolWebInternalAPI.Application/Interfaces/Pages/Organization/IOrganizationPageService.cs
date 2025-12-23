using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;

namespace SchoolWebInternalAPI.Application.Interfaces.Pages
{
    public interface IOrganizationPageService
    {
        Task<ApiResponse<OrganizationPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<OrganizationPageUpdateDto>> UpdateAsync(
            OrganizationPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default);
    }
}
