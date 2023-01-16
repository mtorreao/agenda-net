using AgendaNet.Domain.Commands;
using AgendaNet.Domain.DTOs;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using AgendaNet.Domain.Services;
using AutoMapper;
using Flunt.Notifications;

namespace AgendaNet.Domain.Handlers;

public class AuthHandler : Notifiable<Notification>, IHandler<SignUpCommand>, IHandler<SignInCommand>
{
  private readonly IIdentityRepository _repository;
  private readonly IAuthService _authService;
  private readonly IMapper _mapper;

  public AuthHandler(IIdentityRepository repository, IMapper mapper, IAuthService authService)
  {
    _repository = repository;
    _mapper = mapper;
    _authService = authService;
  }

  public ICommandResult Handle(SignUpCommand command)
  {
    command.Validate();
    if (!command.IsValid)
    {
      command.AddNotifications(command.Notifications);
      return new GenericCommandResult(false, "Dados inválidos", command.Notifications);
    }

    var emailIsTaken = _repository.Users.GetByEmail(command.Email!);
    if (emailIsTaken != null)
    {
      command.AddNotification("Email", "Email já está em uso");
      return new GenericCommandResult(false, "Email já está em uso", command.Notifications);
    }

    _repository.Users.Create(_mapper.Map<User>(command));

    // Clear password for security reasons
    command.Password = null;

    var user = _repository.Users.GetByEmail(command.Email!);

    return new GenericCommandResult(true, "Novo usuário foi criado", new CreateUserDTO()
    {
      User = _mapper.Map<UserDTO>(user),
      Token = _authService.GenerateToken(user)
    });
  }

  public ICommandResult Handle(SignInCommand command)
  {
    command.Validate();
    if (!command.IsValid)
    {
      AddNotifications(command.Notifications);
      return new GenericCommandResult(false, "Dados inválidos", command.Notifications);
    }

    var user = _repository.Users.GetByEmail(command.Email!);
    if (user == null)
    {
      AddNotification("Email", "Usuário não encontrado");
      return new GenericCommandResult(false, "Usuário não encontrado", command.Notifications);
    }

    var isAValidUser = _authService.ValidateCredentials(user, command.Password!);
    if (!isAValidUser)
    {
      AddNotification("Password", "Usuário ou senha inválidos");
      return new GenericCommandResult(false, "Usuário ou senha inválidos", command.Notifications);
    }

    var token = _authService.GenerateToken(user);

    return new GenericCommandResult(true, "Login feito com sucesso", new CreateUserDTO() {
      User = _mapper.Map<UserDTO>(user),
      Token = token
    });
  }
}

