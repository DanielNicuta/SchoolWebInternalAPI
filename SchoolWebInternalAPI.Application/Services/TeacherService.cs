using AutoMapper;
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

        public async Task<List<TeacherResponseDto>> GetAllTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return _mapper.Map<List<TeacherResponseDto>>(teachers);
        }

        public async Task<TeacherResponseDto?> GetTeacherByIdAsync(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            return teacher == null ? null : _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<TeacherResponseDto> CreateTeacherAsync(TeacherCreateDto dto)
        {
            var teacher = _mapper.Map<Teacher>(dto);

            var created = await _teacherRepository.AddAsync(teacher);

            return _mapper.Map<TeacherResponseDto>(created);
        }

        public async Task<bool> UpdateTeacherAsync(TeacherUpdateDto dto)
        {
            var existing = await _teacherRepository.GetByIdAsync(dto.Id);
            if (existing == null)
            {
                return false;
            }

            _mapper.Map(dto, existing);

            return await _teacherRepository.UpdateAsync(existing);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _teacherRepository.DeleteAsync(id);
        }
    }
}
