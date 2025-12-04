using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class FooterContentService : IFooterContentService
    {
        private readonly IFooterPageRepository _repository;
        private readonly IMapper _mapper;

        public FooterContentService(IFooterPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<FooterContentResponseDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
                return ApiResponse<FooterContentResponseDto>.NotFound("Footer content not found.");

            var dto = _mapper.Map<FooterContentResponseDto>(entity);
            return ApiResponse<FooterContentResponseDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<FooterContentResponseDto>> UpdateAsync(
            FooterContentUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<FooterPage>(dto);
            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<FooterContentResponseDto>(saved);

            return ApiResponse<FooterContentResponseDto>.SuccessResponse(
                responseDto,
                "Footer content updated successfully.");
        }
    }
}
