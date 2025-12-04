using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class HomePageService : IHomePageService
    {
        private readonly IHomePageRepository _repository;
        private readonly IMapper _mapper;

        public HomePageService(IHomePageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HomePageResponseDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
                return ApiResponse<HomePageResponseDto>.NotFound("Home page content not yet created.");

            var dto = _mapper.Map<HomePageResponseDto>(entity);

            return ApiResponse<HomePageResponseDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<HomePageResponseDto>> UpdateAsync(
            HomePageUpdateDto dto, 
            string? updatedBy = null,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<HomePage>(dto);

            entity.LastUpdatedAt = DateTime.UtcNow;
            entity.LastUpdatedBy = updatedBy;
            entity.IsPublished = true;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<HomePageResponseDto>(saved);

            return ApiResponse<HomePageResponseDto>.SuccessResponse(responseDto, "Home page updated successfully.", 200);
        }
    }
}
