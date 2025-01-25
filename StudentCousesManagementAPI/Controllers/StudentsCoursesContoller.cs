using Microsoft.AspNetCore.Mvc;
using StudentCousesManagementAPI.Services;

namespace StudentCousesManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsCoursesContoller : ControllerBase
    {
        private readonly IStudentCourseService _studentCourseService;

        public StudentsCoursesContoller(IStudentCourseService studentCourseService)
        {
            _studentCourseService = studentCourseService;
        }
        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudentInCourse(int studentId, int courseId)
        {
            try
            {
                await _studentCourseService.EnrollStudentForCourse(studentId, courseId);
                return Ok();
            }
            catch (NullReferenceException ex)
            {

                return NotFound(ex.Message);
            }
        }
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawStudentFromCourse(int studentId, int courseId)
        {
            try
            {
                await _studentCourseService.WithdrawStudentFromCourse(studentId, courseId);
                return Ok();
            }
            catch (NullReferenceException ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
