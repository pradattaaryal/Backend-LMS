using Domain.Entities;
using MediatR;

namespace Presentation.CQRS.CommandHandler.AuthCommand
{
    public class SignInCommand:IRequest<bool>
    {
        public UserDto userDto { get; set; }
 
    }
}
