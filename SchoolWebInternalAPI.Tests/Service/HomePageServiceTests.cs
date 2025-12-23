using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;

namespace SchoolWebInternalAPI.Tests.Pages
{
    public class HomePageServiceTests
    {
        private readonly Mock<IHomePageRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly HomePageService _service;

        public HomePageServiceTests()
        {
            _repoMock = new Mock<IHomePageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new HomePageService(_repoMock.Object, _mapperMock.Object);
        }

        // --------------------------------------------------------
        // GET TESTS
        // --------------------------------------------------------
        
        [Fact]
        public async Task GetAsync_ReturnsNotFound_WhenNoEntityExists()
        {
            // Arrange
            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((HomePage?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetAsync_ReturnsSuccess_WhenEntityExists()
        {
            var entity = new HomePage { HeroTitle = "Welcome" };
            var dto = new HomePageResponseDto { HeroTitle = "Welcome" };

            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<HomePageResponseDto>(entity))
                .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Welcome", result.Data!.HeroTitle);
        }

        // --------------------------------------------------------
        // UPDATE TESTS
        // --------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_MapsAndUpsertsContent()
        {
            // Arrange
            var updateDto = new HomePageUpdateDto { HeroTitle = "New Title" };
            var mappedEntity = new HomePage { HeroTitle = "New Title" };
            var savedEntity = new HomePage { HeroTitle = "New Title", IsPublished = true };
            var responseDto = new HomePageResponseDto { HeroTitle = "New Title" };

            _mapperMock
                .Setup(m => m.Map<HomePage>(updateDto))
                .Returns(mappedEntity);

            _repoMock
                .Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(savedEntity);

            _mapperMock
                .Setup(m => m.Map<HomePageResponseDto>(savedEntity))
                .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "test-user");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("New Title", result.Data!.HeroTitle);
            Assert.Equal("Home page updated successfully.", result.Message);

            // Entity audit fields must be set
            Assert.True(mappedEntity.IsPublished);
            Assert.Equal("test-user", mappedEntity.LastUpdatedBy);
        }
    }
}
