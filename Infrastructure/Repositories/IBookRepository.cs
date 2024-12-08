using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookRepository
    {
        Task<int> AddBookAsync(Book book);                // Return the new Book ID
        Task<bool> UpdateBookAsync(Book book);            // Return true if update is successful
        Task<bool> DeleteBookAsync(int bookId);           // Return true if delete is successful
        Task<Book> GetBookByIdAsync(int bookId);          // Return a single Book entity
        Task<IEnumerable<Book>> GetAllBooksAsync();       // Return all Book entities as IEnumerable
    }
}
