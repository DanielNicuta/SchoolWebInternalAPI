using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class OrganizationPageService : IOrganizationPageService
    {
        private readonly IOrganizationPageRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationPageService(IOrganizationPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrganizationPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
            {
                return ApiResponse<OrganizationPageUpdateDto>.SuccessResponse(
                    new OrganizationPageUpdateDto(),
                    "Organization page is empty.");
            }

            var dto = _mapper.Map<OrganizationPageUpdateDto>(entity);
            return ApiResponse<OrganizationPageUpdateDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<OrganizationPageUpdateDto>> UpdateAsync(
            OrganizationPageUpdateDto dto,
            string? updatedBy,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<OrganizationPage>(dto);

            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<OrganizationPageUpdateDto>(saved);

            return ApiResponse<OrganizationPageUpdateDto>.SuccessResponse(
                responseDto,
                "Organization page updated successfully.");
        }
    }
}
