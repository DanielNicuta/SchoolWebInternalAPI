using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Application.Interfaces.Pages;

namespace SchoolWebInternalAPI.Api.Controllers.Cms
{
    [ApiController]
    [Route("api/cms/history")]
    public class HistoryPageController : ControllerBase
    {
        private readonly IHistoryPageService _service;

        public HistoryPageController(IHistoryPageService service)
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
            HistoryPageUpdateDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _service.UpdateAsync(dto, "System", cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
