using StudentCousesManagementAPI.DTOs;

namespace StudentCousesManagementAPI.Services
{
    public interface IStudentService
    {
        StudentDto GetStudentById(int id);
        StudentDto GetStudentByName(string name);
        Task<int> CreateStudent(string name);
        Task<bool> UpdateStudent(StudentDto studentDto);
        Task DeleteStudent(int id);
    }
}
