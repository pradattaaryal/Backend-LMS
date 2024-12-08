using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();    // Return IEnumerable<BookDto>
        Task<BookDto> GetBookByIdAsync(int bookId);       // Return a single BookDto
        Task<int> CreateBookAsync(BookDto bookDto);       // Return the new Book ID
        Task<bool> UpdateBookAsync(BookDto bookDto);      // Return true if update is successful
        Task<bool> DeleteBookAsync(int bookId);           // Return true if delete is successful
    }
}
