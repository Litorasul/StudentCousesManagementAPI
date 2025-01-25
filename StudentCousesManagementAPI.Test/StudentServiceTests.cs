using Microsoft.EntityFrameworkCore;
using StudentCousesManagementAPI.Data;
using StudentCousesManagementAPI.Data.Models;
using StudentCousesManagementAPI.DTOs;
using StudentCousesManagementAPI.Services;
using Xunit;

namespace StudentCousesManagementAPI.Test
{
    public class StudentServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentService _studentService;
        private readonly IStudentCourseService _studentCourseService;

        public StudentServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);
            _studentCourseService = new StudentCourseService(_context);
            _studentService = new StudentService(_context, _studentCourseService);
        }

        [Fact]
        public async Task GetStudentById_ShouldWork()
        {
            await PopulateDatabase();
            var student = _studentService.GetStudentById(33);
            Assert.NotNull(student);
            Assert.Equal("A", student.Name);
        }
        [Fact]
        public async Task GetStudentByName_ShouldWork()
        {
            await PopulateDatabase();
            var student = _studentService.GetStudentByName("B");
            Assert.NotNull(student);
            Assert.Equal(55, student.Id);
        }
        [Fact]
        public async Task CreateStudent_ShouldWork()
        {
            await _studentService.CreateStudent("S");
            var student = _context.Students.Where(s => s.Name == "S").FirstOrDefault();

            Assert.NotNull(student);
        }

        [Fact]
        public async Task UpdateStudent_ShouldWork()
        {
            await PopulateDatabase();
            var updated = new StudentDto
            {
                Id = 33,
                Name = "S"
            };
            await _studentService.UpdateStudent(updated);
            var student = _context.Students.Where(s => s.Name == "S").FirstOrDefault();

            Assert.NotNull(student);
        }
        [Fact]
        public async Task DeleteStudent_ShouldWork()
        {
            await PopulateDatabase();
            await _studentService.DeleteStudent(33);
            var student = _context.Students.Where(s => s.Name == "A").FirstOrDefault();
            Assert.Null(student);
        }

        private async Task PopulateDatabase()
        {
            var student1 = new Student
            {
                Id = 33,
                Name = "A"
            };
            var student2 = new Student
            {
                Id = 55,
                Name = "B"
            };
            await _context.Students.AddAsync(student1);
            await _context.Students.AddAsync(student2);
            await _context.SaveChangesAsync();
        }

    }
}
