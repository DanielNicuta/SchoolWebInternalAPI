using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Tests.Pages.Organization
{
    public class OrganizationPageServiceTests
    {
        private readonly Mock<IOrganizationPageRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly OrganizationPageService _service;

        public OrganizationPageServiceTests()
        {
            _repoMock = new Mock<IOrganizationPageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new OrganizationPageService(_repoMock.Object, _mapperMock.Object);
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
                .ReturnsAsync((OrganizationPage?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.IsType<OrganizationPageUpdateDto>(result.Data);
            Assert.Equal("Organization page is empty.", result.Message);
        }

        [Fact]
        public async Task GetAsync_ReturnsMappedDto_When_PageExists()
        {
            // Arrange
            var entity = new OrganizationPage
            {
                Title = "School Structure",
                DescriptionHtml = "<p>Our organizational structure...</p>"
            };

            var dto = new OrganizationPageUpdateDto
            {
                Title = "School Structure",
                DescriptionHtml = "<p>Our organizational structure...</p>"
            };

            _repoMock
                .Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<OrganizationPageUpdateDto>(entity))
                .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("School Structure", result.Data!.Title);
        }

        // ---------------------------------------------------------
        // UPDATE TESTS
        // ---------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_MapsDto_UpsertsEntity_AndReturnsSuccess()
        {
            // Arrange
            var updateDto = new OrganizationPageUpdateDto
            {
                Title = "Updated Structure",
                DescriptionHtml = "<p>Updated content...</p>"
            };

            var mappedEntity = new OrganizationPage
            {
                Title = "Updated Structure",
                DescriptionHtml = "<p>Updated content...</p>"
            };

            var savedEntity = new OrganizationPage
            {
                Title = "Updated Structure",
                DescriptionHtml = "<p>Updated content...</p>"
            };

            var responseDto = new OrganizationPageUpdateDto
            {
                Title = "Updated Structure",
                DescriptionHtml = "<p>Updated content...</p>"
            };

            _mapperMock
                .Setup(m => m.Map<OrganizationPage>(updateDto))
                .Returns(mappedEntity);

            _repoMock
                .Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(savedEntity);

            _mapperMock
                .Setup(m => m.Map<OrganizationPageUpdateDto>(savedEntity))
                .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "admin");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Updated Structure", result.Data!.Title);
            Assert.Equal("Organization page updated successfully.", result.Message);
        }
    }
}
