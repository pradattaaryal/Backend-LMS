using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<int> AddStudentAsync(Student student); // Returns the new Student ID
        Task<bool> UpdateStudentAsync(Student student); // Returns true if update is successful
        Task<bool> DeleteStudentAsync(int studentId); // Returns true if delete is successful
        Task<Student> GetStudentByIdAsync(int studentId); // Returns the Student object
        Task<IEnumerable<Student>> GetAllStudentsAsync(); // Returns a list of Students
    }
}
