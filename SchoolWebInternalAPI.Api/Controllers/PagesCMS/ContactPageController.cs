using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Application.Interfaces.Pages;

namespace SchoolWebInternalAPI.Api.Controllers.Cms
{
    [ApiController]
    [Route("api/page/contact")]
    public class ContactPageController : ControllerBase
    {
        private readonly IContactPageService _service;

        public ContactPageController(IContactPageService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _service.GetAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            [FromBody] ContactPageUpdateDto dto,
            CancellationToken cancellationToken)
        {
            // later you can pass User.Identity?.Name instead of "System"
            var result = await _service.UpdateAsync(dto, "System", cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
