using Moq;
using AutoMapper;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Services;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Domain.Entities;

namespace SchoolWebInternalAPI.Tests
{
    public class TeacherServiceTests
    {
        private readonly Mock<ITeacherRepository> _teacherRepoMock;
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherServiceTests()
        {
            _teacherRepoMock = new Mock<ITeacherRepository>();

            // AutoMapper config for tests
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Teacher, TeacherResponseDto>();
                cfg.CreateMap<TeacherCreateDto, Teacher>();
                cfg.CreateMap<TeacherUpdateDto, Teacher>();
            });

            _mapper = config.CreateMapper();

            _teacherService = new TeacherService(_teacherRepoMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllTeachersAsync_ReturnsMappedDtos()
        {
            var teachers = new List<Teacher>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe" }
            };

            _teacherRepoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(teachers);

            var result = await _teacherService.GetAllTeachersAsync();

            Assert.Single(result);
            Assert.IsType<List<TeacherResponseDto>>(result);
        }

        [Fact]
        public async Task GetTeacherByIdAsync_ReturnsMappedDto()
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
        public async Task CreateTeacherAsync_ReturnsCreatedDto()
        {
            var dto = new TeacherCreateDto { FirstName = "Alex", LastName = "Pop" };

            var createdEntity = new Teacher
            {
                Id = 10,
                FirstName = "Alex",
                LastName = "Pop"
            };

            _teacherRepoMock
                .Setup(r => r.AddAsync(It.IsAny<Teacher>()))
                .ReturnsAsync(createdEntity);

            var result = await _teacherService.CreateTeacherAsync(dto);

            Assert.NotNull(result);
            Assert.Equal(10, result.Id);
        }

        [Fact]
        public async Task UpdateTeacherAsync_ReturnsTrue()
        {
            var dto = new TeacherUpdateDto
            {
                Id = 1,
                FirstName = "New",
                LastName = "Name"
            };

            _teacherRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Teacher>()))
                .ReturnsAsync(true);

            var result = await _teacherService.UpdateTeacherAsync(dto);

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
