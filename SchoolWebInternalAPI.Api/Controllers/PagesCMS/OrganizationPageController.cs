using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Application.Interfaces.Pages;

namespace SchoolWebInternalAPI.Api.Controllers.Cms
{
    [ApiController]
    [Route("api/cms/organization")]
    public class OrganizationPageController : ControllerBase
    {
        private readonly IOrganizationPageService _service;

        public OrganizationPageController(IOrganizationPageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(
            OrganizationPageUpdateDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(dto, "System", cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
