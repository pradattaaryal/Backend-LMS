using Application.Interfaces;
using Application.Queries;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.QueryHandlers
{
    public class AuthorQueryHandler :
        IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>,  // Return IEnumerable<AuthorDto>
        IRequestHandler<GetAuthorByIdQuery, AuthorDto>  // Return a single AuthorDto
    {
        private readonly IAuthorService _authorService;

        public AuthorQueryHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Handle GetAllAuthorsQuery - returns list of AuthorDto
        public async Task<IEnumerable<AuthorDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _authorService.GetAllAuthorsAsync();
        }

        // Handle GetAuthorByIdQuery - returns a single AuthorDto
        public async Task<AuthorDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _authorService.GetAuthorByIdAsync(request.AuthorId);
        }
    }
}
