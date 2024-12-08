using Domain.Entities;
using MediatR;

namespace Application.Commands
{

    public class CreateBookCommand : IRequest<int>
    {
        public BookDto BookDto { get; set; }
    }


    public class UpdateBookCommand : IRequest<bool>
    {
        public BookDto BookDto { get; set; }
    }


    public class DeleteBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
    }
}
