using StudentCousesManagementAPI.Enums;

namespace StudentCousesManagementAPI.DTOs
{
    public class CourseDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int Capacity { get; set; }
        public ProgressStage Stage { get; set; }
    }
}
