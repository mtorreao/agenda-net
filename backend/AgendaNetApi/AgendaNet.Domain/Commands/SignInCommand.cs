using Flunt.Notifications;
using Flunt.Validations;

namespace AgendaNet.Domain.Commands;

public class SignInCommand : Notifiable<Notification>, ICommand
{
  public SignInCommand()
  {
  }

  public SignInCommand(string email, string password)
  {
    Email = email;
    Password = password;
  }

  public string? Email { get; set; }
  public string? Password { get; set; }

  public void Validate()
  {
    AddNotifications(
      new Contract<Notification>()
        .Requires()
        .IsEmailOrEmpty(Email, "Email", "Email inválido")
        .IsNotNullOrEmpty(Password, "Password", "Senha é obrigatória")
        .IsGreaterOrEqualsThan(Password, 8, "Password", "Senha deve ter no mínimo 8 caracteres")
    );
  }
}