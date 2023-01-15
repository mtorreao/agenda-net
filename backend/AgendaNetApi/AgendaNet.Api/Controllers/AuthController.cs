using System.Net;
using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
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
  public ActionResult<Token> SignIn(
    [FromBody] User user,
    [FromServices] ILogger<AuthController> logger,
    [FromServices] AccessManager accessManager)
  {
    logger.LogInformation("User: {0}, requesting token for app access", user.Email);
    if (user is not null && accessManager.ValidateCredentials(user))
    {
      return accessManager.GenerateToken(user);
    }
    else
    {
      return new UnauthorizedResult();
    }
  }

  [AllowAnonymous]
  [Route("signup")]
  [HttpPost]
  [ProducesResponseType(typeof(Token), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
  public GenericCommandResult SignUp(
    [FromBody] SignUpCommand user,
    [FromServices] IHandler<SignUpCommand> handler)
  {
    return (GenericCommandResult)handler.Handle(user);
  }
}
