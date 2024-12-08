using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Queries;
using System.Threading.Tasks;
using Presentation.CQRS.QueryHandler.BookQuery;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery { BookId = id });
            if (book == null)
                return NotFound(new { message = "Book not found" });

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
                return BadRequest(new { message = "Invalid book data" });

            var newBookId = await _mediator.Send(new CreateBookCommand { BookDto = bookDto });

            if (newBookId <= 0)
                return BadRequest(new { message = "Failed to create book" });

            return CreatedAtAction(nameof(GetBookById), new { id = newBookId }, bookDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            if (id != bookDto.BookId)
                return BadRequest(new { message = "Book ID mismatch" });

            var result = await _mediator.Send(new UpdateBookCommand { BookDto = bookDto });

            if (!result)
                return NotFound(new { message = "Book not found or update failed" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _mediator.Send(new DeleteBookCommand { BookId = id });

            if (!result)
                return NotFound(new { message = "Book not found or could not be deleted" });

            return NoContent();
        }
    }
}
