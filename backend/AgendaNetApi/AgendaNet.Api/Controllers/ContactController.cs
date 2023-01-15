using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("Bearer")]
public class ContactController : ControllerBase
{
  [HttpGet]
  public IEnumerable<Contact> GetAll([FromServices] IRepository repository)
  {
    return repository.Contacts.GetAll();
  }

  [HttpPost]
  public GenericCommandResult Create([FromBody] CreateContactCommand command, [FromServices] IHandler<CreateContactCommand> handler)
  {
    return (GenericCommandResult)handler.Handle(command);
  }
}
