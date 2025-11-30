using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.DTOs;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Application.Mapping;
using SchoolWebInternalAPI.Domain.Entities;
using SchoolWebInternalAPI.Infrastructure;
using SchoolWebInternalAPI.Infrastructure.Repositories;
using SchoolWebInternalAPI.Infrastructure.Services;
using Xunit;

public class TeacherServiceTests
{
    private readonly IMapper _mapper;

    public TeacherServiceTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetAll_Returns_Teachers()
    {
        var mockRepo = new Mock<ITeacherRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Teacher> {
            new Teacher { Id = 1, FirstName = "Ion", LastName = "Popescu" }
        });

        var service = new TeacherService(mockRepo.Object, _mapper);

        var list = await service.GetAllAsync();

        Assert.NotEmpty(list);
        Assert.Contains(list, t => t.FirstName == "Ion");
    }
}
