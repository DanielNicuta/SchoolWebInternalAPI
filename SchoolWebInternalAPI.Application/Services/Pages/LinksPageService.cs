using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class LinksPageService : ILinksPageService
    {
        private readonly ILinksPageRepository _repository;
        private readonly IMapper _mapper;

        public LinksPageService(ILinksPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LinksPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
            {
                return ApiResponse<LinksPageUpdateDto>.SuccessResponse(
                    new LinksPageUpdateDto(),
                    "Links page is empty.");
            }

            var dto = _mapper.Map<LinksPageUpdateDto>(entity);
            return ApiResponse<LinksPageUpdateDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<LinksPageUpdateDto>> UpdateAsync(
            LinksPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<LinksPage>(dto);

            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<LinksPageUpdateDto>(saved);

            return ApiResponse<LinksPageUpdateDto>.SuccessResponse(
                responseDto,
                "Links page updated successfully.");
        }
    }
}
