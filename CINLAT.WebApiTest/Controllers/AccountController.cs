using Microsoft.AspNetCore.Hosting.Server;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MediatR;
using CINLAT.WebApiTest.Application.DTOs;
using static CINLAT.WebApiTest.Application.Infrastructure.Login.Commands.LoginCommand;
using CINLAT.WebApiTest.Application.Infrastructure.Login;

namespace CINLAT.WebApiTest.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        //[AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Profile>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var command = new LoginCommandRequest(request);
            var resultado = await _sender.Send(command, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado) : Unauthorized();

        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return "Pong";
        }



    }
}
