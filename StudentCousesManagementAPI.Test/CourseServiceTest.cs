using Microsoft.EntityFrameworkCore;
using StudentCousesManagementAPI.Data;
using StudentCousesManagementAPI.Data.Models;
using StudentCousesManagementAPI.Enums;
using StudentCousesManagementAPI.Services;

namespace StudentCousesManagementAPI.Test
{
    public class CourseServiceTest
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourseService _courseService;
        private readonly IStudentCourseService _studentCourseService;

        public CourseServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _context = new ApplicationDbContext(options);
            _studentCourseService = new StudentCourseService(_context);
            _courseService = new CourseService(_context, _studentCourseService);
        }
        [Fact]
        public async Task GetCourseById_ShouldWork()
        {
            await PopulateDatabase();
            var course = _courseService.GetCourseById(20);
            Assert.NotNull(course);
            Assert.Equal("Foo", course.Name);
        }
        [Fact]
        public async Task GetCourseByName_ShouldWork()
        {
            await PopulateDatabase();
            var course = _courseService.GetCourseByName("Boo");
            Assert.NotNull(course);
            Assert.Equal(30, course.Id);
        }
        [Fact]
        public async Task GetCourseProgressStage_ShouldWork()
        {
            await PopulateDatabase();
            var stage = _courseService.GetCourseProgressStage(30);

            Assert.Equal(ProgressStage.InProgress, stage);
        }
        [Fact]
        public async Task GetAllAvailableCourses_ShouldWork()
        {
            await PopulateDatabase();
            var courses = _courseService.GetAllAvailableCourses();
            Assert.NotNull(courses);
            Assert.Single(courses);
        }

        private async Task PopulateDatabase()
        {
            var course1 = new Course
            {
                Id = 20,
                Name = "Foo",
                StartDate = DateOnly.Parse("2025-02-04"),
                EndDate = DateOnly.Parse("2025-03-04"),
                Capacity = 10,
                Stage = Enums.ProgressStage.Created
            };
            var course2 = new Course
            {
                Id = 30,
                Name = "Boo",
                StartDate = DateOnly.Parse("2025-02-04"),
                EndDate = DateOnly.Parse("2025-03-04"),
                Capacity = 10,
                Stage = Enums.ProgressStage.InProgress
            };
            var course3 = new Course
            {
                Id = 40,
                Name = "Woo",
                StartDate = DateOnly.Parse("2025-02-04"),
                EndDate = DateOnly.Parse("2025-03-04"),
                Capacity = 10,
                Stage = Enums.ProgressStage.Finished
            };
            await _context.Courses.AddAsync(course1);
            await _context.Courses.AddAsync(course2);
            await _context.Courses.AddAsync(course3);
            await _context.SaveChangesAsync();
        }
    }
}
