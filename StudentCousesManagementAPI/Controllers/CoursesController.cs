using Microsoft.AspNetCore.Mvc;
using StudentCousesManagementAPI.DTOs;
using StudentCousesManagementAPI.Enums;
using StudentCousesManagementAPI.Services;

namespace StudentCousesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("id")]
        public ActionResult<CourseDto> GetCourseById(int id)
        {
            try
            {
                var course = _courseService.GetCourseById(id);
                return Ok(course);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("name")]
        public ActionResult<CourseDto> GetCourseByName(string name)
        {
            var course = _courseService.GetCourseByName(name);
            return Ok(course);
        }
        [HttpGet("stage")]
        public ActionResult<ProgressStage> GetCourseProgressStage(int id)
        {
            try
            {
                var stage = _courseService.GetCourseProgressStage(id);
                return Ok(stage);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("available")]
        public ActionResult<List<CourseDto>> GetAllAvailableCourses()
        {
            var courses = _courseService.GetAllAvailableCourses();
            return Ok(courses);
        }

        [HttpGet("all-by-stage")]
        public ActionResult<List<CourseDto>> GetAllCoursesByStage(ProgressStage stage)
        {
            var courses = _courseService.GetAllCoursesByStage(stage);
            return Ok(courses);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateCourse(CourseDto courseDto)
        {
            if (courseDto == null
                || string.IsNullOrEmpty(courseDto.Name)
                || courseDto.Capacity < 1
                || courseDto.EndDate <= courseDto.StartDate) { return BadRequest(); };

            var courseId = await _courseService.CreateCourse(courseDto);
            return Ok(courseId);
        }

        [HttpPut("update")]
        public async Task<ActionResult<bool>> UpdateStudent(CourseDto courseDto)
        {
            if (courseDto == null
                || string.IsNullOrEmpty(courseDto.Name)
                || courseDto.Capacity < 1
                || courseDto.EndDate <= courseDto.StartDate) { return BadRequest(); };

            try
            {
                var isUpdated = await _courseService.UpdateCourse(courseDto);
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
                await _courseService.DeleteCourse(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
