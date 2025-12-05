using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.PagesCSM;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _repository;
        private readonly IMapper _mapper;

        public SiteSettingsService(ISiteSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves site-wide settings for the CMS.
        /// If no settings exist, returns an empty editable DTO.
        /// </summary>
        public async Task<ApiResponse<SiteSettingsUpdateDto>> GetAsync(
            CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
            {
                return ApiResponse<SiteSettingsUpdateDto>.SuccessResponse(
                    new SiteSettingsUpdateDto(),
                    "No site settings found. Returning default editable values.");
            }

            var dto = _mapper.Map<SiteSettingsUpdateDto>(entity);

            return ApiResponse<SiteSettingsUpdateDto>.SuccessResponse(dto);
        }

        /// <summary>
        /// Creates or updates the site settings.
        /// </summary>
        public async Task<ApiResponse<SiteSettingsUpdateDto>> UpdateAsync(
            SiteSettingsUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<SiteSettings>(dto);

            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<SiteSettingsUpdateDto>(saved);

            return ApiResponse<SiteSettingsUpdateDto>.SuccessResponse(
                responseDto,
                "Site settings updated successfully.");
        }
    }
}
