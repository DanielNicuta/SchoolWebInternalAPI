using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using SchoolWebInternalAPI.Application.Interfaces.Pages;

namespace SchoolWebInternalAPI.Api.Controllers.Cms
{
    [ApiController]
    [Route("api/cms/settings")]
    public class SiteSettingsController : ControllerBase
    {
        private readonly ISiteSettingsService _service;

        public SiteSettingsController(ISiteSettingsService service)
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
            SiteSettingsUpdateDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(dto, "System", cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
