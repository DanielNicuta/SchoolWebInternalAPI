using SchoolWebInternalAPI.Application.Common.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Services
{
    public class TeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public Task<List<Teacher>> GetAllAsync() =>
            _teacherRepository.GetAllAsync();

        public Task<Teacher?> GetByIdAsync(int id) =>
            _teacherRepository.GetByIdAsync(id);

        public Task AddAsync(Teacher teacher) =>
            _teacherRepository.AddAsync(teacher);

        public Task UpdateAsync(Teacher teacher) =>
            _teacherRepository.UpdateAsync(teacher);

        public Task DeleteAsync(Teacher teacher) =>
            _teacherRepository.DeleteAsync(teacher);
    }
}
