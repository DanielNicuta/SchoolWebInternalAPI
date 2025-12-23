using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Services.Pages
{
    public class ContactPageService : IContactPageService
    {
        private readonly IContactPageRepository _repository;
        private readonly IMapper _mapper;

        public ContactPageService(IContactPageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ContactPageResponseDto>> GetAsync(CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetAsync(cancellationToken);

            if (entity == null)
            {
                // You could also initialize a default ContactPage here if you want
                return ApiResponse<ContactPageResponseDto>.NotFound("Contact page content not found.");
            }

            var dto = _mapper.Map<ContactPageResponseDto>(entity);
            return ApiResponse<ContactPageResponseDto>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<ContactPageResponseDto>> UpdateAsync(
            ContactPageUpdateDto dto,
            string? updatedBy = null,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<ContactPage>(dto);

            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = updatedBy;

            var saved = await _repository.UpsertAsync(entity, cancellationToken);
            var responseDto = _mapper.Map<ContactPageResponseDto>(saved);

            return ApiResponse<ContactPageResponseDto>.SuccessResponse(
                responseDto,
                "Contact page updated successfully.");
        }
    }
}
