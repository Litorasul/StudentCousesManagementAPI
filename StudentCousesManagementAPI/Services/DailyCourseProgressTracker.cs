
using StudentCousesManagementAPI.Enums;

namespace StudentCousesManagementAPI.Services
{
    public class DailyCourseProgressTracker : BackgroundService
    {
        private readonly PeriodicTimer _periodicTimer = new PeriodicTimer(TimeSpan.FromDays(1));
        private readonly IServiceProvider _serviceProvider;

        public DailyCourseProgressTracker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _periodicTimer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var courseService = scope.ServiceProvider.GetService<CourseService>();
                    var createdCourses = courseService.GetAllCoursesByStage(ProgressStage.Created);
                    var inProgressCourses = courseService.GetAllCoursesByStage(ProgressStage.InProgress);
                    foreach (var createdCourse in createdCourses)
                    {
                        if (createdCourse.StartDate >= DateOnly.FromDateTime(DateTime.Now))
                        {
                            createdCourse.Stage = ProgressStage.InProgress;
                            await courseService.UpdateCourse(createdCourse);
                        }
                    }
                    foreach (var inProgressCourse in inProgressCourses)
                    {
                        if (inProgressCourse.EndDate >= DateOnly.FromDateTime(DateTime.Now))
                        {
                            inProgressCourse.Stage = ProgressStage.Finished;
                            await courseService.UpdateCourse(inProgressCourse);
                        }
                    }
                }
            }
        }
    }
}
