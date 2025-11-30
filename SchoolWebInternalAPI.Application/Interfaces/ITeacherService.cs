using SchoolWebInternalAPI.Application.DTOs.Teachers;

namespace SchoolWebInternalAPI.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeacherResponseDto>> GetAllTeachersAsync();
        Task<TeacherResponseDto?> GetTeacherByIdAsync(int id);
        Task<TeacherResponseDto> CreateTeacherAsync(TeacherCreateDto teacher);
        Task<TeacherResponseDto> UpdateTeacherAsync(TeacherUpdateDto teacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
