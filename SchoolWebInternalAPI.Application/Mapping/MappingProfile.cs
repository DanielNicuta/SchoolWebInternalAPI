using AutoMapper;
using SchoolWebInternalAPI.Application.DTOs;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Teacher, TeacherDto>().ReverseMap();
        }
    }
}
