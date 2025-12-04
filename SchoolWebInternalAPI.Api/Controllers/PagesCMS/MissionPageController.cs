using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Application.Interfaces.Pages;

namespace SchoolWebInternalAPI.Api.Controllers.Cms
{
    [ApiController]
    [Route("api/cms/mission")]
    public class MissionPageController : ControllerBase
    {
        private readonly IMissionPageService _service;

        public MissionPageController(IMissionPageService service)
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
            MissionPageUpdateDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(dto, "System", cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
