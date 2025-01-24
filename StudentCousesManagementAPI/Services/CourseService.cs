using StudentCousesManagementAPI.Data;
using StudentCousesManagementAPI.Data.Models;
using StudentCousesManagementAPI.DTOs;
using StudentCousesManagementAPI.Enums;

namespace StudentCousesManagementAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentCourseService _studentCourseService;

        public CourseService(ApplicationDbContext context, IStudentCourseService studentCourseService)
        {
            _context = context;
            _studentCourseService = studentCourseService;
        }

        public CourseDto GetCourseById(int id)
        {
            var course = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {id} does not exist in the Database!"); }

            return new CourseDto 
            { 
                Id = course.Id, 
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Capacity = course.Capacity,
                Stage = course.Stage
            };
        }

        public CourseDto GetCourseByName(string name)
        {
            var course = _context.Courses.Where(c => c.Name == name).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {name} does not exist in the Database!"); }

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Capacity = course.Capacity,
                Stage = course.Stage
            };
        }

        public ProgressStage GetCourseProgressStage(int id)
        {
            var course = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {id} does not exist in the Database!"); }

            return course.Stage;
        }
        public List<CourseDto> GetAllAvailableCourses()
        {
            var availableCourses = _context.Courses.Where(c => c.Capacity > 0 && c.Stage == ProgressStage.Created).ToList();
            return availableCourses.Select(ac => new CourseDto
            {
                Id = ac.Id,
                Name = ac.Name,
                Description = ac.Description,
                StartDate = ac.StartDate,
                EndDate = ac.EndDate,
                Capacity = ac.Capacity,
                Stage = ac.Stage
            }).ToList();
        }

        public List<CourseDto> GetAllCoursesByStage(ProgressStage stage)
        {
            var availableCourses = _context.Courses.Where(c => c.Stage == stage).ToList();
            return availableCourses.Select(ac => new CourseDto
            {
                Id = ac.Id,
                Name = ac.Name,
                Description = ac.Description,
                StartDate = ac.StartDate,
                EndDate = ac.EndDate,
                Capacity = ac.Capacity,
                Stage = ac.Stage
            }).ToList();
        }

        public List<StudentDto> GetAllStudentsForCourse(int id)
        {
            var students = _context.StudentCourses
                .Where(sc => sc.CourseId == id)
                .Select(sc => sc.Student)
                .ToList();

            return students.Select(student => new StudentDto
            {
                Id = student.Id,
                Name = student.Name
            }).ToList();
        }
        public async Task<int> CreateCourse(CourseDto courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                StartDate = courseDto.StartDate,
                EndDate = courseDto.EndDate,
                Capacity = courseDto.Capacity,
                Stage = ProgressStage.Created
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return course.Id;
        }

        public async Task<bool> UpdateCourse(CourseDto courseDto)
        {
            var course = _context.Courses.Where(c => c.Id == courseDto.Id).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {courseDto.Id} does not exist in the Database!"); }

            course.Name = courseDto.Name;
            course.Description = courseDto.Description;
            course.StartDate = courseDto.StartDate;
            course.EndDate = courseDto.EndDate;
            course.Capacity = courseDto.Capacity;
            course.Stage = courseDto.Stage;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeleteCourse(int id)
        {
            var course = _context.Courses.Where(c => c.Id == id).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {id} does not exist in the Database!"); }

            await _studentCourseService.DeleteAllStudentCoursesByStudentOrCourse(false, id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

        }
    }
}
