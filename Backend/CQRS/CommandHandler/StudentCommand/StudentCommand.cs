using MediatR;
using Domain.Entities;

namespace Application.Commands
{
    // Command to create a new student
    public class CreateStudentCommand : IRequest<int>
    {
        public StudentDto StudentDto { get; set; }
    }

    // Command to update an existing student
    public class UpdateStudentCommand : IRequest<bool>
    {
        public StudentDto StudentDto { get; set; }
    }

    // Command to delete a student
    public class DeleteStudentCommand : IRequest<bool>
    {
        public int StudentId { get; set; }
    }
}
