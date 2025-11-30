using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Services;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Tests
{
    public class TeacherServiceTests
    {
        private readonly Mock<ITeacherRepository> _teacherRepoMock;
        private readonly ITeacherService _teacherService;

        public TeacherServiceTests()
        {
            _teacherRepoMock = new Mock<ITeacherRepository>();
            _teacherService = new TeacherService(_teacherRepoMock.Object);
        }

        [Fact]
        public async Task GetAllTeachersAsync_ReturnsList()
        {
            // Arrange
            var expected = new List<Teacher>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe" }
            };

            _teacherRepoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(expected);

            // Act
            var result = await _teacherService.GetAllTeachersAsync();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetTeacherByIdAsync_ReturnsTeacher()
        {
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe" };

            _teacherRepoMock
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(teacher);

            var result = await _teacherService.GetTeacherByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
        }

        [Fact]
        public async Task CreateTeacherAsync_ReturnsCreatedTeacher()
        {
            var newTeacher = new Teacher { FirstName = "Alex", LastName = "Pop" };
            var createdTeacher = new Teacher { Id = 5, FirstName = "Alex", LastName = "Pop" };

            _teacherRepoMock
                .Setup(r => r.AddAsync(newTeacher))
                .ReturnsAsync(createdTeacher);

            var result = await _teacherService.CreateTeacherAsync(newTeacher);

            Assert.Equal(5, result.Id);
        }

        [Fact]
        public async Task UpdateTeacherAsync_ReturnsTrue()
        {
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe" };

            _teacherRepoMock
                .Setup(r => r.UpdateAsync(teacher))
                .ReturnsAsync(true);

            var result = await _teacherService.UpdateTeacherAsync(teacher);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteTeacherAsync_ReturnsTrue()
        {
            _teacherRepoMock
                .Setup(r => r.DeleteAsync(1))
                .ReturnsAsync(true);

            var result = await _teacherService.DeleteTeacherAsync(1);

            Assert.True(result);
        }
    }
}
