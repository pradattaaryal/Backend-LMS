using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Queries;
using Domain.Entities;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery { AuthorId = id });
            if (author == null)
                return NotFound(new { message = "Author not found" });

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
        {
            if (authorDto == null)
                return BadRequest(new { message = "Invalid author data" });

            var newAuthorId = await _mediator.Send(new CreateAuthorCommand { AuthorDto = authorDto });

            if (newAuthorId <= 0)
                return BadRequest(new { message = "Failed to create author" });

            return CreatedAtAction(nameof(GetAuthorById), new { id = newAuthorId }, authorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDto authorDto)
        {
            if (id != authorDto.AuthorId)
                return BadRequest(new { message = "Author ID mismatch" });

            var result = await _mediator.Send(new UpdateAuthorCommand { AuthorDto = authorDto });

            if (!result)
                return NotFound(new { message = "Author not found or update failed" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand { AuthorId = id });

            if (!result)
                return NotFound(new { message = "Author not found or could not be deleted." });
                return NoContent();
        }

    }
}
