using Flunt.Notifications;
using Flunt.Validations;

namespace AgendaNet.Domain.Commands
{
  public class UpdateContactCommand : Notifiable<Notification>, ICommand
  {
    public UpdateContactCommand()
    {
    }

    public UpdateContactCommand(string id, string name, string email, string phone, string? userId)
    {
      Id = id;
      Name = name;
      Email = email;
      Phone = phone;
      UserId = userId;
    }

    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? UserId { get; set; }

    public void Validate()
    {
      AddNotifications(
        new Contract<Notification>()
          .Requires()
          .IsNotNullOrEmpty(Id, "Id", "Id é obrigatório")
          .IsNotNullOrEmpty(Name, "Name", "Nome é obrigatório")
          .IsEmailOrEmpty(Email, "Email", "Email inválido")
          .IsNotNullOrEmpty(Phone, "Phone", "Telefone é obrigatório")
          .IsNotNullOrEmpty(UserId, "UserId", "Usuário é obrigatório")
      );
    }
  }
}