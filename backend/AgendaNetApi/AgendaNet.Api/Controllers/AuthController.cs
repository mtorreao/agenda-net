using System.Net;
using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Handlers;
using AgendaNet.Infra.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  [AllowAnonymous]
  [Route("signin")]
  [HttpPost]
  [ProducesResponseType(typeof(Token), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
  public GenericCommandResult SignIn(
    [FromBody] SignInCommand command,
    [FromServices] IHandler<SignInCommand> handler)
  {
    return (GenericCommandResult)handler.Handle(command);
  }

  [AllowAnonymous]
  [Route("signup")]
  [HttpPost]
  [ProducesResponseType(typeof(Token), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
  public GenericCommandResult SignUp(
    [FromBody] SignUpCommand command,
    [FromServices] IHandler<SignUpCommand> handler)
  {
    return (GenericCommandResult)handler.Handle(command);
  }
}
