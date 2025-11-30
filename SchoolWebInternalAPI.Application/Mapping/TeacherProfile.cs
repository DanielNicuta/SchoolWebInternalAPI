using AutoMapper;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Mappings;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        // Domain → DTO
        CreateMap<Teacher, TeacherResponseDto>();

        // DTO → Domain
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<TeacherUpdateDto, Teacher>();
    }
}
