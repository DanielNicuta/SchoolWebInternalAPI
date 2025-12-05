using AutoMapper;
using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        // --------------------------------------------------------------
        // GET ALL
        // --------------------------------------------------------------
        public async Task<ApiResponse<List<TeacherResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var teachers = await _teacherRepository.GetAllAsync(cancellationToken);

            var dtos = _mapper.Map<List<TeacherResponseDto>>(teachers);

            return ApiResponse<List<TeacherResponseDto>>
                .SuccessResponse(dtos);
        }

        // --------------------------------------------------------------
        // GET BY ID
        // --------------------------------------------------------------
        public async Task<ApiResponse<TeacherResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id, cancellationToken);

            if (teacher == null)
            {
                return ApiResponse<TeacherResponseDto>
                    .NotFound($"Teacher with id {id} was not found.");
            }

            var dto = _mapper.Map<TeacherResponseDto>(teacher);

            return ApiResponse<TeacherResponseDto>
                .SuccessResponse(dto);
        }

        // --------------------------------------------------------------
        // CREATE
        // --------------------------------------------------------------
        public async Task<ApiResponse<TeacherResponseDto>> CreateAsync(
            TeacherCreateDto dto,
            CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Teacher>(dto);

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            var created = await _teacherRepository.AddAsync(entity, cancellationToken);

            var responseDto = _mapper.Map<TeacherResponseDto>(created);

            return ApiResponse<TeacherResponseDto>
                .SuccessResponse(responseDto, "Teacher created successfully.");
        }

        // --------------------------------------------------------------
        // UPDATE
        // --------------------------------------------------------------
        public async Task<ApiResponse<TeacherResponseDto>> UpdateAsync(
            TeacherUpdateDto dto,
            CancellationToken cancellationToken = default)
        {
            var existing = await _teacherRepository.GetByIdAsync(dto.Id, cancellationToken);

            if (existing == null)
            {
                return ApiResponse<TeacherResponseDto>
                    .NotFound($"Teacher with id {dto.Id} was not found.");
            }

            // Update fields
            _mapper.Map(dto, existing);

            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _teacherRepository.UpdateAsync(existing, cancellationToken);
            if (updated == null)
            {
                return ApiResponse<TeacherResponseDto>
                    .Fail("Failed to update teacher.", 500);
            }

            var responseDto = _mapper.Map<TeacherResponseDto>(updated);

            return ApiResponse<TeacherResponseDto>
                .SuccessResponse(responseDto, "Teacher updated successfully.");
        }

        // --------------------------------------------------------------
        // DELETE
        // --------------------------------------------------------------
        public async Task<ApiResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var success = await _teacherRepository.DeleteAsync(id, cancellationToken);

            if (!success)
            {
                return ApiResponse<bool>
                    .NotFound($"Teacher with id {id} was not found.");
            }

            return ApiResponse<bool>
                .SuccessResponse(true, "Teacher deleted successfully.");
        }
    }
}
