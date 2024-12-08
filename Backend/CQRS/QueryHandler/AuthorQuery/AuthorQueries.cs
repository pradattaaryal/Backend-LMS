using MediatR;
using Domain.Entities;

namespace Application.Queries
{
    
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDto>> { }

   
    public class GetAuthorByIdQuery : IRequest<AuthorDto>
    {
        public int AuthorId { get; set; }
    }
}
