using AutoMapper;
using Moq;
using SchoolWebInternalAPI.Application.Services.Pages;
using SchoolWebInternalAPI.Application.Interfaces.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Tests.Pages
{
    public class ContactPageServiceTests
    {
        private readonly Mock<IContactPageRepository> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ContactPageService _service;

        public ContactPageServiceTests()
        {
            _repoMock = new Mock<IContactPageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ContactPageService(_repoMock.Object, _mapperMock.Object);
        }

        // -----------------------------
        // GET TESTS
        // -----------------------------

        [Fact]
        public async Task GetAsync_ReturnsNotFound_WhenEntityIsNull()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync((ContactPage?)null);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Contact page content not found.", result.Message);
        }

        [Fact]
        public async Task GetAsync_ReturnsSuccess_WhenEntityExists()
        {
            // Arrange
            var entity = new ContactPage { Title = "Contact Us" };
            var dto = new ContactPageResponseDto { Title = "Contact Us" };

            _repoMock.Setup(r => r.GetAsync(It.IsAny<CancellationToken>()))
                     .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<ContactPageResponseDto>(entity))
                       .Returns(dto);

            // Act
            var result = await _service.GetAsync();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(dto.Title, result.Data!.Title);
        }

        // -----------------------------
        // UPDATE TESTS
        // -----------------------------

        [Fact]
        public async Task UpdateAsync_MapsAndUpsertsEntity_ReturnsUpdatedDto()
        {
            // Arrange
            var updateDto = new ContactPageUpdateDto
            {
                Title = "New Title",
                Email = "test@test.com"
            };

            var mappedEntity = new ContactPage
            {
                Title = "New Title",
                Email = "test@test.com"
            };

            var savedEntity = new ContactPage
            {
                Title = "New Title",
                Email = "test@test.com"
            };

            var responseDto = new ContactPageResponseDto
            {
                Title = "New Title",
                Email = "test@test.com"
            };

            _mapperMock.Setup(m => m.Map<ContactPage>(updateDto))
                       .Returns(mappedEntity);

            _repoMock.Setup(r => r.UpsertAsync(mappedEntity, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(savedEntity);

            _mapperMock.Setup(m => m.Map<ContactPageResponseDto>(savedEntity))
                       .Returns(responseDto);

            // Act
            var result = await _service.UpdateAsync(updateDto, "admin");

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("New Title", result.Data!.Title);
            Assert.Equal("Contact page updated successfully.", result.Message);
        }
    }
}
