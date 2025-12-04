using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Application.Interfaces;

namespace SchoolWebInternalAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teachers
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _teacherService.GetAllAsync(cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/teachers/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _teacherService.GetByIdAsync(id, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/teachers
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _teacherService.CreateAsync(dto, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/teachers/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherUpdateDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest("URL id does not match body id.");
            }

            var result = await _teacherService.UpdateAsync(dto, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/teachers/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _teacherService.DeleteAsync(id, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
