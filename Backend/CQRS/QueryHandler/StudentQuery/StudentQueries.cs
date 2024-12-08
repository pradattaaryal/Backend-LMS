using MediatR;
using Domain.Entities;

namespace Application.Queries
{
    // Query to get all students
    public class GetAllStudentsQuery : IRequest<IEnumerable<StudentDto>> { }

    // Query to get a student by ID
    public class GetStudentByIdQuery : IRequest<StudentDto>
    {
        public int StudentId { get; set; }
    }
}
