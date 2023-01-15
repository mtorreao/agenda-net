using Flunt.Notifications;
using Flunt.Validations;

namespace AgendaNet.Domain.Commands
{
  public class DeleteContactCommand : Notifiable<Notification>, ICommand
  {
    public DeleteContactCommand()
    {
    }

    public string? Id { get; set; }
    public string? UserId { get; set; }

    public void Validate()
    {
      AddNotifications(
        new Contract<Notification>()
          .Requires()
          .IsNotNullOrEmpty(Id, "Id", "Id é obrigatório")
          .IsNotNullOrEmpty(UserId, "UserId", "Usuário é obrigatório")
      );
    }
  }
}