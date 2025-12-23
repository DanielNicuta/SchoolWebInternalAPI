using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Tests.Pages.Mission
{
    public class MissionPageServiceTests
    {
        private readonly Mock<IMissionPageRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MissionPageService _service;

        public MissionPageServiceTests()
        {
            _repoMock = new Mock<IMissionPageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new MissionPageService(_repoMock.Object, _mapperMock.Object);
        }

        // ---------------------------------------------------------
        // GET TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task GetAsync_ReturnsEmptyDto_When_PageDoesNotExist()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((MissionPage?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.IsType<MissionPageUpdateDto>(result.Data);
            Assert.Equal("Mission page is empty.", result.Message);
        }

        [Fact]
        public async Task GetAsync_ReturnsMappedDto_When_PageExists()
        {
            // Arrange
            var entity = new MissionPage
            {
                Title = "Our Mission",
                MissionHtml = "<p>We aim to inspire...</p>"
            };

            var dto = new MissionPageUpdateDto
            {
                Title = "Our Mission",
                MissionHtml = "<p>We aim to inspire...</p>"
            };

            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<MissionPageUpdateDto>(entity))
                .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Our Mission", result.Data!.Title);
        }

        // ---------------------------------------------------------
        // UPDATE TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_MapsDto_UpsertsEntity_AndReturnsSuccess()
        {
            // Arrange
            var updateDto = new MissionPageUpdateDto
            {
                Title = "Updated Mission",
                MissionHtml = "<p>Updated...</p>"
            };

            var mappedEntity = new MissionPage
            {
                Title = "Updated Mission",
                MissionHtml = "<p>Updated...</p>"
            };

            var savedEntity = new MissionPage
            {
                Title = "Updated Mission",
                MissionHtml = "<p>Updated...</p>"
            };

            var responseDto = new MissionPageUpdateDto
            {
                Title = "Updated Mission",
                MissionHtml = "<p>Updated...</p>"
            };

            _mapperMock
                .Setup(m => m.Map<MissionPage>(updateDto))
                .Returns(mappedEntity);

            _repoMock
                .Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(savedEntity);

            _mapperMock
                .Setup(m => m.Map<MissionPageUpdateDto>(savedEntity))
                .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "admin");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Updated Mission", result.Data!.Title);
            Assert.Equal("Mission page updated successfully.", result.Message);
        }
    }
}
