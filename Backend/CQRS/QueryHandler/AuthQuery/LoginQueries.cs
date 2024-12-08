using MediatR;

namespace Presentation.CQRS.QueryHandler.AuthQuery
{
    public class LoginQueries:IRequest<String>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
