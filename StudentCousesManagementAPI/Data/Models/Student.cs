using System.ComponentModel.DataAnnotations;

namespace StudentCousesManagementAPI.Data.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        public string Name { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
