using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolWebInternalAPI.API.Responses;
using SchoolWebInternalAPI.Application.DTOs.Teachers;
using SchoolWebInternalAPI.Application.Interfaces;

namespace SchoolWebInternalAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            var dto = _mapper.Map<List<TeacherResponseDto>>(teachers);

            return Ok(ApiResponse<List<TeacherResponseDto>>.Ok(dto));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);

            if (teacher == null)
                return NotFound(ApiResponse<string>.Fail("Teacher not found"));

            var dto = _mapper.Map<TeacherResponseDto>(teacher);
            return Ok(ApiResponse<TeacherResponseDto>.Ok(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto dto)
        {
            var result = await _teacherService.CreateTeacherAsync(dto);
            return Ok(ApiResponse<TeacherResponseDto>.Ok(result, "Teacher created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TeacherUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ApiResponse<string>.Fail("URL id does not match body id"));

            var success = await _teacherService.UpdateTeacherAsync(dto);

            if (!success)
                return NotFound(ApiResponse<string>.Fail("Teacher not found"));

            return Ok(ApiResponse<string>.Ok("Teacher updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _teacherService.DeleteTeacherAsync(id);

            if (!success)
                return NotFound(ApiResponse<string>.Fail("Teacher not found"));

            return Ok(ApiResponse<string>.Ok("Teacher deleted successfully"));
        }
    }
}
