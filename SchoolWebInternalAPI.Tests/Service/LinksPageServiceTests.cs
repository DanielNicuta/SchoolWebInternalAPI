using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Tests.Pages.Links
{
    public class LinksPageServiceTests
    {
        private readonly Mock<ILinksPageRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LinksPageService _service;

        public LinksPageServiceTests()
        {
            _repoMock = new Mock<ILinksPageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new LinksPageService(_repoMock.Object, _mapperMock.Object);
        }

        // ---------------------------------------------------------
        // GET TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task GetAsync_ReturnsEmptyDto_When_NoPageExists()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((LinksPage?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.IsType<LinksPageUpdateDto>(result.Data);
            Assert.Equal("Links page is empty.", result.Message);
        }

        [Fact]
        public async Task GetAsync_ReturnsMappedDto_When_PageExists()
        {
            // Arrange
            var entity = new LinksPage
            {
                Title = "Useful Education Links",
                IntroHtml = "<p>Welcome to links...</p>"
            };

            var dto = new LinksPageUpdateDto
            {
                Title = "Useful Education Links",
                IntroHtml = "<p>Welcome to links...</p>"
            };

            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<LinksPageUpdateDto>(entity))
                .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Useful Education Links", result.Data!.Title);
        }

        // ---------------------------------------------------------
        // UPDATE TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_MapsDto_UpsertsEntity_ReturnsSuccess()
        {
            // Arrange
            var updateDto = new LinksPageUpdateDto
            {
                Title = "Updated Links",
                IntroHtml = "<p>Updated...</p>"
            };

            var mappedEntity = new LinksPage
            {
                Title = "Updated Links",
                IntroHtml = "<p>Updated...</p>"
            };

            var savedEntity = new LinksPage
            {
                Title = "Updated Links",
                IntroHtml = "<p>Updated...</p>"
            };

            var responseDto = new LinksPageUpdateDto
            {
                Title = "Updated Links",
                IntroHtml = "<p>Updated...</p>"
            };

            _mapperMock
                .Setup(m => m.Map<LinksPage>(updateDto))
                .Returns(mappedEntity);

            _repoMock
                .Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(savedEntity);

            _mapperMock
                .Setup(m => m.Map<LinksPageUpdateDto>(savedEntity))
                .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "admin");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Updated Links", result.Data!.Title);
            Assert.Equal("Links page updated successfully.", result.Message);
        }
    }
}
