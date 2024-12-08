using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();     // Get all students
        Task<StudentDto> GetStudentByIdAsync(int studentId);      // Get a single student by ID
        Task<int> CreateStudentAsync(StudentDto studentDto);      // Create a student and return the new ID
        Task<bool> UpdateStudentAsync(StudentDto studentDto);     // Update a student and return true if successful
        Task<bool> DeleteStudentAsync(int studentId);             // Delete a student and return true if successful
    }
}
