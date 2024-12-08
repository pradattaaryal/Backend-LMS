using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync(); // Return IEnumerable<AuthorDto>
        Task<AuthorDto> GetAuthorByIdAsync(int authorId);  // Return a single AuthorDto
        Task<int> CreateAuthorAsync(AuthorDto authorDto);  // Return the new Author ID
        Task<bool> UpdateAuthorAsync(AuthorDto authorDto); // Return true if update is successful
        Task<bool> DeleteAuthorAsync(int authorId);        // Return true if delete is successful
    }
}
