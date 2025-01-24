using StudentCousesManagementAPI.DTOs;
using StudentCousesManagementAPI.Enums;

namespace StudentCousesManagementAPI.Services
{
    public interface ICourseService
    {
        CourseDto GetCourseById(int id);
        CourseDto GetCourseByName(string name);
        ProgressStage GetCourseProgressStage(int id);
        List<StudentDto> GetAllStudentsForCourse(int id);
        List<CourseDto> GetAllAvailableCourses();
        List<CourseDto> GetAllCoursesByStage(ProgressStage stage);
        Task<int> CreateCourse(CourseDto courseDto);
        Task<bool> UpdateCourse(CourseDto courseDto);
        Task DeleteCourse(int id);
    }
}
