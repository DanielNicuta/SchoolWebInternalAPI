using SchoolWebInternalAPI.Application.Common.Models;
using SchoolWebInternalAPI.Application.DTOs.Teachers;

namespace SchoolWebInternalAPI.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<ApiResponse<List<TeacherResponseDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<TeacherResponseDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ApiResponse<TeacherResponseDto>> CreateAsync(TeacherCreateDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<TeacherResponseDto>> UpdateAsync(TeacherUpdateDto dto, CancellationToken cancellationToken = default);
        Task<ApiResponse<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
