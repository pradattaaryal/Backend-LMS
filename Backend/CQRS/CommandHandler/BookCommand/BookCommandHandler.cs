using Application.Interfaces;
using Application.Commands;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers
{
    public class BookCommandHandler :
        IRequestHandler<CreateBookCommand, int>,   // Return the new Book ID
        IRequestHandler<DeleteBookCommand, bool>, // Return true if delete is successful
        IRequestHandler<UpdateBookCommand, bool>  // Return true if update is successful
    {
        private readonly IBookService _bookService;

        public BookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Handle CreateBookCommand - returns new Book ID
        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookService.CreateBookAsync(request.BookDto);
            }
            catch
            {
                return 0; // Return 0 to indicate failure
            }
        }

        // Handle DeleteBookCommand - returns true if delete is successful
        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            return await _bookService.DeleteBookAsync(request.BookId);
        }

        // Handle UpdateBookCommand - returns true if update is successful
        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookService.UpdateBookAsync(request.BookDto);
            }
            catch
            {
                return false; // Return false if update fails
            }
        }
    }
}
