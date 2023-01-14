using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using Flunt.Notifications;

namespace AgendaNet.Domain.Handlers
{
  public class ContactHandler : Notifiable<Notification>, IHandler<CreateContactCommand>
  {
    private readonly IRepository _repository;

    public ContactHandler(IRepository repository)
    {
      _repository = repository;
    }

    public ICommandResult Handle(CreateContactCommand command)
    {
      command.Validate();
      if (!command.IsValid)
      {
        AddNotifications(command.Notifications);
        return new GenericCommandResult(false, "Invalid data", command.Notifications);
      }

      var contact = new Contact(command.Name!, command.Email!, command.Phone!, new DateTime(), new Guid());
      _repository.Contacts.Create(contact);

      return new GenericCommandResult(true, "Contact created", contact);
    }
  }

}