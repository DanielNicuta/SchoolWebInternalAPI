using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Application.Interfaces;

namespace SchoolWebInternalAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/teacher
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers); // Already DTOs
        }

        // GET: api/teacher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);

            if (teacher == null)
                return NotFound();

            return Ok(teacher); // Already DTO
        }

        // POST: api/teacher
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _teacherService.CreateTeacherAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/teacher/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TeacherUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("URL ID does not match body ID.");

            var updated = await _teacherService.UpdateTeacherAsync(dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/teacher/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _teacherService.DeleteTeacherAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
