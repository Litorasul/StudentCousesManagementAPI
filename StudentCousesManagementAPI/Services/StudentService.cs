using StudentCousesManagementAPI.Data;
using StudentCousesManagementAPI.Data.Models;
using StudentCousesManagementAPI.DTOs;

namespace StudentCousesManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public StudentDto GetStudentById(int id)
        {
            var student = _context.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null) { throw new NullReferenceException($"Student with ID: {id} does not exist in the Database!"); }

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name
            };
        }

        public StudentDto GetStudentByName(string name)
        {
            var student = _context.Students.Where(s => s.Name == name).FirstOrDefault();
            if (student == null) { throw new NullReferenceException($"Student with Name: {name} does not exist in the Database!"); }

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name
            };
        }
        public async Task<int> CreateStudent(string name)
        {
            var student = new Student { Name = name };

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> UpdateStudent(StudentDto studentDto)
        {
            var student = _context.Students.Where(s => s.Id == studentDto.Id).FirstOrDefault();
            if (student == null) { throw new NullReferenceException($"Student with ID: {studentDto.Id} does not exist in the Database!"); }

            student.Name = studentDto.Name;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task DeleteStudent(int id)
        {
            var student = _context.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student == null) { throw new NullReferenceException($"Student with ID: {id} does not exist in the Database!"); }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

    }
}
