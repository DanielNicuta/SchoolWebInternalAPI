using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Tests.Pages.History
{
    public class HistoryPageServiceTests
    {
        private readonly Mock<IHistoryPageRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly HistoryPageService _service;

        public HistoryPageServiceTests()
        {
            _repoMock = new Mock<IHistoryPageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new HistoryPageService(_repoMock.Object, _mapperMock.Object);
        }

        // ---------------------------------------------------------
        // GET TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task GetAsync_ReturnsEmptyEditableDto_When_PageIsNull()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((HistoryPage?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.IsType<HistoryPageUpdateDto>(result.Data);
            Assert.Equal("History page is empty.", result.Message);
        }

        [Fact]
        public async Task GetAsync_ReturnsMappedDto_When_PageExists()
        {
            // Arrange
            var entity = new HistoryPage
            {
                Title = "About Our School"
            };

            var dto = new HistoryPageUpdateDto
            {
                Title = "About Our School"
            };

            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<HistoryPageUpdateDto>(entity))
                .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("About Our School", result.Data!.Title);
        }

        // ---------------------------------------------------------
        // UPDATE TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_MapsDto_UpsertsEntity_ReturnsSuccess()
        {
            // Arrange
            var updateDto = new HistoryPageUpdateDto
            {
                Title = "Updated History"
            };

            var mappedEntity = new HistoryPage
            {
                Title = "Updated History"
            };

            var savedEntity = new HistoryPage
            {
                Title = "Updated History"
            };

            var responseDto = new HistoryPageUpdateDto
            {
                Title = "Updated History"
            };

            _mapperMock
                .Setup(m => m.Map<HistoryPage>(updateDto))
                .Returns(mappedEntity);

            _repoMock
                .Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(savedEntity);

            _mapperMock
                .Setup(m => m.Map<HistoryPageUpdateDto>(savedEntity))
                .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "admin");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Updated History", result.Data!.Title);
            Assert.Equal("History page updated successfully.", result.Message);
        }
    }
}
