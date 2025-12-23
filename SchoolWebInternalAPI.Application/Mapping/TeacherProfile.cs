using AutoMapper;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Mapping
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherResponseDto>();

            CreateMap<TeacherCreateDto, Teacher>();

            CreateMap<TeacherUpdateDto, Teacher>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // we set Id from existing entity
        }
    }
}
