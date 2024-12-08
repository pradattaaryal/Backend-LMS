using Application.Interfaces;
using Application.Queries;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers
{
    public class StudentQueryHandler :
        IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>,  // Return IEnumerable<StudentDto>
        IRequestHandler<GetStudentByIdQuery, StudentDto>  // Return a single StudentDto
    {
        private readonly IStudentService _studentService;

        public StudentQueryHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // Handle GetAllStudentsQuery - returns list of StudentDto
        public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetAllStudentsAsync();
        }

        // Handle GetStudentByIdQuery - returns a single StudentDto
        public async Task<StudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentByIdAsync(request.StudentId);
        }
    }
}
