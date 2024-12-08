using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.CQRS.CommandHandler.AuthCommand;
using Presentation.CQRS.QueryHandler.AuthQuery;
using System.Threading.Tasks;

namespace practices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        

        public AuthController(IMediator mediator )
        {
            _mediator = mediator;
             
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginQueries loginDto)
        {
            try
            {
                var token = await _mediator.Send(loginDto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserDto user)
        {
            try
            {
                var result = await _mediator.Send(new SignInCommand { userDto = user });
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
