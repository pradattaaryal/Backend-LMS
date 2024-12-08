using Domain.Entities;
using MediatR;

namespace Presentation.CQRS.QueryHandler.BookQuery
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookDto>> { }


    public class GetBookByIdQuery : IRequest<BookDto>
    {
        public int BookId { get; set; }
    }
}
