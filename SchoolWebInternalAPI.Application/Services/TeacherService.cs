using AutoMapper;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repo;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<TeacherResponseDto>> GetAllTeachersAsync()
        {
            var teachers = await _repo.GetAllAsync();
            return _mapper.Map<List<TeacherResponseDto>>(teachers);
        }

        public async Task<TeacherResponseDto?> GetTeacherByIdAsync(int id)
        {
            var teacher = await _repo.GetByIdAsync(id);
            return _mapper.Map<TeacherResponseDto>(teacher);
        }

        public async Task<TeacherResponseDto> CreateTeacherAsync(TeacherCreateDto dto)
        {
            var entity = _mapper.Map<Teacher>(dto);

            var saved = await _repo.AddAsync(entity);

            return _mapper.Map<TeacherResponseDto>(saved);
        }

        public async Task<bool> UpdateTeacherAsync(TeacherUpdateDto dto)
        {
            var entity = _mapper.Map<Teacher>(dto);
            return await _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteTeacherAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
