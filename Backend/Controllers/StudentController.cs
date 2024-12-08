using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Queries;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _mediator.Send(new GetAllStudentsQuery());
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _mediator.Send(new GetStudentByIdQuery { StudentId = id });
            if (student == null)
                return NotFound(new { message = "Student not found" });

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
        {
            if (studentDto == null)
                return BadRequest(new { message = "Invalid student data" });

            var newStudentId = await _mediator.Send(new CreateStudentCommand { StudentDto = studentDto });

            if (newStudentId <= 0)
                return BadRequest(new { message = "Failed to create student" });

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudentId }, studentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (id != studentDto.StudentId)
                return BadRequest(new { message = "Student ID mismatch" });

            var result = await _mediator.Send(new UpdateStudentCommand { StudentDto = studentDto });

            if (!result)
                return NotFound(new { message = "Student not found or update failed" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { StudentId = id });

            if (!result)
                return NotFound(new { message = "Student not found or could not be deleted" });

            return NoContent();
        }
    }
}
