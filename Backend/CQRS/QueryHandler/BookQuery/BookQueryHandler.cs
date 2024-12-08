using Application.Interfaces;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Presentation.CQRS.QueryHandler.BookQuery;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers
{
    public class BookQueryHandler :
        IRequestHandler<GetAllBooksQuery, IEnumerable<BookDto>>,  // Return IEnumerable<BookDto>
        IRequestHandler<GetBookByIdQuery, BookDto>  // Return a single BookDto
    {
        private readonly IBookService _bookService;

        public BookQueryHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Handle GetAllBooksQuery - returns list of BookDto
        public async Task<IEnumerable<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookService.GetAllBooksAsync();
        }

        // Handle GetBookByIdQuery - returns a single BookDto
        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _bookService.GetBookByIdAsync(request.BookId);
        }
    }
}
