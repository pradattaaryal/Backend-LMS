using Application.Interfaces;
using Backend.Presentation;
using Domain.Entities;
using MediatR;
using Presentation.Utility;

namespace Presentation.CQRS.QueryHandler.AuthQuery
{
    public class LoginQueryHandler : IRequestHandler<LoginQueries, string>
    {
        private readonly IAuthService _authenticationService;
        private readonly IConfiguration _configuration;
        public LoginQueryHandler(IAuthService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginQueries loginQueries, CancellationToken cancellationToken)
        {
            UserDto userData = await _authenticationService.GetUserByName(loginQueries.Username);

            if (userData != null)
            {
                string hashedPassword = HashUtility.Hash(loginQueries.Password);

                if (hashedPassword == userData.Password)
                {
                    return JwtTokenGenerator.GenerateToken(userData, _configuration);
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
    }
}
