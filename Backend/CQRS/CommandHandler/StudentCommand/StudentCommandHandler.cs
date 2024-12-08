using Application.Interfaces;
using Application.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers
{
    public class StudentCommandHandler :
        IRequestHandler<CreateStudentCommand, int>,   // Return the new Student ID
        IRequestHandler<DeleteStudentCommand, bool>,  // Return true if delete is successful
        IRequestHandler<UpdateStudentCommand, bool>   // Return true if update is successful
    {
        private readonly IStudentService _studentService;

        public StudentCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // Handle CreateStudentCommand - returns new Student ID
        public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _studentService.CreateStudentAsync(request.StudentDto);
            }
            catch
            {
                return 0; // Return 0 to indicate failure
            }
        }

        // Handle DeleteStudentCommand - returns true if delete is successful
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _studentService.DeleteStudentAsync(request.StudentId);
            }
            catch
            {
                return false; // Return false if deletion fails
            }
        }

        // Handle UpdateStudentCommand - returns true if update is successful
        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _studentService.UpdateStudentAsync(request.StudentDto);
            }
            catch
            {
                return false; // Return false if update fails
            }
        }
    }
}
