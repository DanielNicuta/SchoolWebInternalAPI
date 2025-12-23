using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class HistoryPageService : IHistoryPageService
    {
        private readonly IHistoryPageRepository _repository;
        private readonly IMapper _mapper;

        public HistoryPageService(IHistoryPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HistoryPageUpdateDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
            {
                // Return empty editable object (for CMS UI)
                return ApiResponse<HistoryPageUpdateDto>.SuccessResponse(
                    new HistoryPageUpdateDto(),
                    "History page is empty.");
            }

            var dto = _mapper.Map<HistoryPageUpdateDto>(entity);
            return ApiResponse<HistoryPageUpdateDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<HistoryPageUpdateDto>> UpdateAsync(
            HistoryPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<HistoryPage>(dto);

            entity.UpdatedAt = DateTime.UtcNow;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<HistoryPageUpdateDto>(saved);

            return ApiResponse<HistoryPageUpdateDto>.SuccessResponse(
                responseDto,
                "History page updated successfully.");
        }
    }
}
