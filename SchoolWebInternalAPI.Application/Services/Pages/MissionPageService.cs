using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class MissionPageService : IMissionPageService
    {
        private readonly IMissionPageRepository _repository;
        private readonly IMapper _mapper;

        public MissionPageService(IMissionPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<MissionPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
            {
                return ApiResponse<MissionPageUpdateDto>.SuccessResponse(
                    new MissionPageUpdateDto(),
                    "Mission page is empty.");
            }

            var dto = _mapper.Map<MissionPageUpdateDto>(entity);
            return ApiResponse<MissionPageUpdateDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<MissionPageUpdateDto>> UpdateAsync(
            MissionPageUpdateDto dto,
            string? updatedBy,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<MissionPage>(dto);

            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<MissionPageUpdateDto>(saved);

            return ApiResponse<MissionPageUpdateDto>.SuccessResponse(
                responseDto,
                "Mission page updated successfully.");
        }
    }
}
