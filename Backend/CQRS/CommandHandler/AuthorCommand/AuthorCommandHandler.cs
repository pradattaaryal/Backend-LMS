using Application.Interfaces;
using Application.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.CommandHandlers
{
    public class AuthorCommandHandler :
        IRequestHandler<CreateAuthorCommand, int>,   // Return the new Author ID
        IRequestHandler<DeleteAuthorCommand, bool>,  // Return true if delete is successful
        IRequestHandler<UpdateAuthorCommand, bool>   // Return true if update is successful
    {
        private readonly IAuthorService _authorService;

        public AuthorCommandHandler(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Handle CreateAuthorCommand - returns new Author ID
        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authorService.CreateAuthorAsync(request.AuthorDto);
            }
            catch
            {
                return 0; // Return 0 to indicate failure
            }
        }

        // Handle DeleteAuthorCommand - returns true if delete is successful
        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            
                return await _authorService.DeleteAuthorAsync(request.AuthorId);
            
        }

        // Handle UpdateAuthorCommand - returns true if update is successful
        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authorService.UpdateAuthorAsync(request.AuthorDto);
            }
            catch
            {
                return false; // Return false if update fails
            }
        }
    }
}
