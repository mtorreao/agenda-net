using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using AgendaNet.Domain.Services;
using AutoMapper;
using Flunt.Notifications;

namespace AgendaNet.Domain.Handlers;

public class AuthHandler : Notifiable<Notification>, IHandler<SignUpCommand>
{
  private readonly IIdentityRepository _repository;
  private readonly IHasher _hasher;
  private readonly IMapper _mapper;

  public AuthHandler(IIdentityRepository repository, IHasher hasher, IMapper mapper)
  {
    _repository = repository;
    _hasher = hasher;
    _mapper = mapper;
  }

  public ICommandResult Handle(SignUpCommand command)
  {
    command.Validate();
    if (!command.IsValid)
    {
      AddNotifications(command.Notifications);
      return new GenericCommandResult(false, "Dados inválidos", command.Notifications);
    }

    var password = _hasher.HashPassword(_mapper.Map<SignUpCommand, User>(command), command.Password ?? string.Empty);
    command.Password = password;

    _repository.Users.Create(_mapper.Map<User>(command));

    // Clear password for security reasons
    command.Password = null;

    return new GenericCommandResult(true, "Novo usuário foi criado", command);
  }
}

