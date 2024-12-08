using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    
    public class CreateAuthorCommand : IRequest<int>
    {
        public AuthorDto AuthorDto { get; set; }
    }

   
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public AuthorDto AuthorDto { get; set; }
    }

    
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int AuthorId { get; set; }
    }
}
