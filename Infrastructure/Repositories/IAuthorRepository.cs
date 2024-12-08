using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAuthorRepository
{
    Task<int> AddAuthorAsync(Author author); // Returns the new Author ID
    Task<bool> UpdateAuthorAsync(Author author); // Returns true if update is successful
    Task<bool> DeleteAuthorAsync(int authorId); // Returns true if delete is successful
    Task<Author> GetAuthorByIdAsync(int authorId); // Returns the Author object
    Task<IEnumerable<Author>> GetAllAuthorsAsync(); // Returns a list of Authors
}
