namespace StudentCousesManagementAPI.Services
{
    public interface IStudentCourseService
    {
        Task EnrollStudentForCourse(int studentId, int courseId);
        Task WithdrawStudentFromCourse(int studentId, int courseId);
        Task DeleteAllStudentCoursesByStudentOrCourse(bool forStudent, int id);
    }
}
