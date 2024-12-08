using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Presentation.CQRS.CommandHandler.AuthCommand
{
    public class SignInHandler : IRequestHandler<SignInCommand, bool>
    {
        private readonly IAuthService _authenticationService;
        public SignInHandler(IAuthService authenticationService) 
        {
            _authenticationService = authenticationService;
        }
         
        public async Task<bool> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            return await _authenticationService.CreateUser(command.userDto);
        }

    }
}
