using Flunt.Notifications;
using Flunt.Validations;

namespace AgendaNet.Domain.Commands
{
  public class CreateContactCommand : Notifiable<Notification>, ICommand
  {
    public CreateContactCommand()
    {
    }

    public CreateContactCommand(string name, string email, string phone)
    {
      Name = name;
      Email = email;
      Phone = phone;
    }

    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }

    public void Validate()
    {
      AddNotifications(
        new Contract<Notification>()
          .Requires()
          .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
          .IsEmailOrEmpty(Email, "Email", "Email inválido")
          .IsNotNullOrEmpty(Phone, "Phone", "Telefone é obrigatório")
      );
    }
  }
}