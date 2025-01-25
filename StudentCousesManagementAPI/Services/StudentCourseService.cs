using StudentCousesManagementAPI.Data;
using StudentCousesManagementAPI.Data.Models;

namespace StudentCousesManagementAPI.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly ApplicationDbContext _context;

        public StudentCourseService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAllStudentCoursesByStudentOrCourse(bool forStudent, int id)
        {
           var studentCourses = _context.StudentCourses.Where(sc => forStudent ? sc.StudentId == id : sc.CourseId == id).ToArray();

            _context.StudentCourses.RemoveRange(studentCourses);
            await _context.SaveChangesAsync();
        }

        public async Task EnrollStudentForCourse(int studentId, int courseId)
        {
            var sc = _context.StudentCourses.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId).FirstOrDefault();
            if (sc != null) { throw new NullReferenceException($"Student with ID: {studentId} is already enrolled in Course with Id: {courseId}!"); }
            var student = _context.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if (student == null) { throw new NullReferenceException($"Student with ID: {studentId} does not exist in the Database!"); }
            var course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {courseId} does not exist in the Database!"); }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            course.Capacity -= 1;
            
            await _context.StudentCourses.AddAsync(studentCourse);
            await _context.SaveChangesAsync();
        }

        public async Task WithdrawStudentFromCourse(int studentId, int courseId)
        {
           var studentCourse = _context.StudentCourses.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId).FirstOrDefault();
            if (studentCourse == null) { throw new NullReferenceException($"Student with ID: {studentId} is not enrolled in Course with Id: {courseId}!"); }
            var course = _context.Courses.Where(c => c.Id == courseId).FirstOrDefault();
            if (course == null) { throw new NullReferenceException($"Course with ID: {courseId} does not exist in the Database!"); }

            course.Capacity += 1;
            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();
        }
    }
}
