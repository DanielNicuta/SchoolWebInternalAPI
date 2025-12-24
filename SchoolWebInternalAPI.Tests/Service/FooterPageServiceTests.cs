using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Tests.Pages.Footer
{
    public class FooterContentServiceTests
    {
        private readonly Mock<IFooterContentRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FooterContentService _service;

        public FooterContentServiceTests()
        {
            _repoMock = new Mock<IFooterContentRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new FooterContentService(_repoMock.Object, _mapperMock.Object);
        }

        // ---------------------------------------
        // GET TESTS
        // ---------------------------------------

        [Fact]
        public async Task GetAsync_ReturnsNotFound_WhenEntityDoesNotExist()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((FooterContent?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Footer content not found.", result.Message);
        }

        [Fact]
        public async Task GetAsync_ReturnsSuccess_WhenEntityExists()
        {
            // Arrange
            var entity = new FooterContent
            {
                FooterText = "© 2025 MySchool"
            };

            var dto = new FooterContentResponseDto
            {
                FooterText = "© 2025 MySchool"
            };

            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<FooterContentResponseDto>(entity))
                .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("© 2025 MySchool", result.Data!.FooterText);
        }

        // ---------------------------------------
        // UPDATE TESTS
        // ---------------------------------------

        [Fact]
        public async Task UpdateAsync_MapsDto_UpsertsEntity_ReturnsSuccess()
        {
            // Arrange
            var updateDto = new FooterContentUpdateDto
            {
                FooterText = "Updated Footer",
                UsefulLinksJson = "[]"
            };

            var mappedEntity = new FooterContent
            {
                FooterText = "Updated Footer",
                UsefulLinksJson = "[]"
            };

            var savedEntity = new FooterContent
            {
                FooterText = "Updated Footer",
                UsefulLinksJson = "[]"
            };

            var responseDto = new FooterContentResponseDto
            {
                FooterText = "Updated Footer",
                UsefulLinksJson = "[]"
            };

            _mapperMock
                .Setup(m => m.Map<FooterContent>(updateDto))
                .Returns(mappedEntity);

            _repoMock
                .Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(savedEntity);

            _mapperMock
                .Setup(m => m.Map<FooterContentResponseDto>(savedEntity))
                .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "admin");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Updated Footer", result.Data!.FooterText);
            Assert.Equal("Footer content updated successfully.", result.Message);
        }
    }
}
