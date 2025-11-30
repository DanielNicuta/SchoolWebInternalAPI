using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.Application.Interfaces;
using SchoolWebInternalAPI.Domain.Entities;

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
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        // GET: api/teacher/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);

            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }

        // POST: api/teacher
        [HttpPost]
        public async Task<IActionResult> CreateTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _teacherService.CreateTeacherAsync(teacher);

            return CreatedAtAction(
                nameof(GetTeacherById),
                new { id = created.Id },
                created
            );
        }

        // PUT: api/teacher/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            if (id != teacher.Id)
                return BadRequest("ID mismatch.");

            var existing = await _teacherService.GetTeacherByIdAsync(id);
            if (existing == null)
                return NotFound();

            var success = await _teacherService.UpdateTeacherAsync(teacher);
            if (!success)
                return StatusCode(500, "Failed to update teacher.");

            return NoContent();
        }

        // DELETE: api/teacher/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var existing = await _teacherService.GetTeacherByIdAsync(id);
            if (existing == null)
                return NotFound();

            var success = await _teacherService.DeleteTeacherAsync(id);
            if (!success)
                return StatusCode(500, "Failed to delete teacher.");

            return NoContent();
        }
    }
}
