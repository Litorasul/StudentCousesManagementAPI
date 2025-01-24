using StudentCousesManagementAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentCousesManagementAPI.Data.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Course Name is required.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "Capacity is required.")]
        public int Capacity { get; set; }
        public ProgressStage Stage { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
