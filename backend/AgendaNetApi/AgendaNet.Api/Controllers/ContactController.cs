using AgendaNet.Domain.Commands;
using AgendaNet.Domain.DTOs;
using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaNet.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize("Bearer")]
public class ContactController : ControllerBase
{
  [HttpGet]
  public IEnumerable<ContactDTO> GetAll([FromServices] IRepository repository, [FromServices] IMapper mapper)
  {
    var userId = User?.Identity?.Name;
    return mapper.Map<IEnumerable<ContactDTO>>(repository.Contacts.GetAll(userId));
  }

  [HttpPost]
  public GenericCommandResult Create([FromBody] CreateContactCommand command, [FromServices] IHandler<CreateContactCommand> handler)
  {
    var userId = User?.Identity?.Name;
    command.UserId = userId;
    return (GenericCommandResult)handler.Handle(command);
  }

  [HttpPatch]
  public GenericCommandResult Update([FromBody] UpdateContactCommand command, [FromServices] IHandler<UpdateContactCommand> handler)
  {
    var userId = User?.Identity?.Name;
    command.UserId = userId;
    return (GenericCommandResult)handler.Handle(command);
  }

  [HttpDelete]
  public GenericCommandResult Delete([FromBody] DeleteContactCommand command, [FromServices] IHandler<DeleteContactCommand> handler)
  {
    var userId = User?.Identity?.Name;
    command.UserId = userId;
    return (GenericCommandResult)handler.Handle(command);
  }

  [HttpGet("{id}")]
  public ActionResult<ContactDTO> GetById([FromRoute] string id, [FromServices] IRepository repository, [FromServices] IMapper mapper)
  {
    var userId = User?.Identity?.Name;
    var contact = mapper.Map<ContactDTO>(repository.Contacts.GetById(Guid.Parse(id), userId));
    if (contact == null)
    {
      return new NotFoundResult();
    }
    return contact;
  }
}
