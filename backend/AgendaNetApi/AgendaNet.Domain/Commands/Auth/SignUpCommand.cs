using Flunt.Notifications;
using Flunt.Validations;

namespace AgendaNet.Domain.Commands;

public class SignUpCommand : Notifiable<Notification>, ICommand
{
  public SignUpCommand()
  {
  }

  public SignUpCommand(string name, string email, string password)
  {
    Name = name;
    Email = email;
    Password = password;
  }

  public string? Name { get; set; }
  public string? Email { get; set; }
  public string? Password { get; set; }

  public void Validate()
  {
    AddNotifications(
      new Contract<Notification>()
        .Requires()
        .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
        .IsNotNullOrEmpty(Email, "Email", "Email é obrigatório")
        .IsEmailOrEmpty(Email, "Email", "Email inválido")
        .IsNotNullOrEmpty(Password, "Password", "Senha é obrigatória")
        .IsGreaterOrEqualsThan(Password, 8, "Password", "Senha deve ter no mínimo 8 caracteres")
    );
  }
}