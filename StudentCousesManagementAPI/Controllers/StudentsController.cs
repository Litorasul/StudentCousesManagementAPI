using Microsoft.AspNetCore.Mvc;
using StudentCousesManagementAPI.DTOs;
using StudentCousesManagementAPI.Services;

namespace StudentCousesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("id")]
        public ActionResult<StudentDto> GetStudentById(int id) 
        {
            try
            {
                var student = _studentService.GetStudentById(id);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("name")]
        public ActionResult<StudentDto> GetStudentByName(string name)
        {
            try
            {
                var student = _studentService.GetStudentByName(name);
                return Ok(student);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateStudent(string name)
        {
            if (string.IsNullOrEmpty(name)) { return BadRequest(); };

            var studentId = await _studentService.CreateStudent(name);
            return Ok(studentId);
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> UpdateStudent(StudentDto studentDto)
        {
            if (studentDto == null || string.IsNullOrEmpty(studentDto.Name)) { return BadRequest(); };

            try
            {
                var isUpdated = await _studentService.UpdateStudent(studentDto);
                return Ok(isUpdated);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentService.DeleteStudent(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
