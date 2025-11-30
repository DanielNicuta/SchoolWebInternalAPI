using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public Task<List<Teacher>> GetAllTeachersAsync()
            => _teacherRepository.GetAllAsync();

        public Task<Teacher?> GetTeacherByIdAsync(int id)
            => _teacherRepository.GetByIdAsync(id);

        public Task<Teacher> CreateTeacherAsync(Teacher teacher)
            => _teacherRepository.AddAsync(teacher);

        public Task<bool> UpdateTeacherAsync(Teacher teacher)
            => _teacherRepository.UpdateAsync(teacher);

        public Task<bool> DeleteTeacherAsync(int id)
            => _teacherRepository.DeleteAsync(id);
    }
}
