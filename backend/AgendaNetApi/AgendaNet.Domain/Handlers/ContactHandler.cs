using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Domain.Handlers
{
  public class ContactHandler : IHandler<CreateContactCommand>, IHandler<DeleteContactCommand>, IHandler<UpdateContactCommand>
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
        return new GenericCommandResult(false, "Dados inválidos", command.Notifications);
      }

      var contact = new Contact(command.Name!, command.Email!, command.Phone!, DateTime.UtcNow, Guid.Parse(command.UserId!));
      _repository.Contacts.Create(contact);

      return new GenericCommandResult(true, "Contact created", contact);
    }

    public ICommandResult Handle(UpdateContactCommand command)
    {
      command.Validate();
      if (!command.IsValid)
      {
        return new GenericCommandResult(false, "Dados inválidos", command.Notifications);
      }

      var contact = _repository.Contacts.GetById(command.Id!, command.UserId!);

      if (contact == null)
      {
        command.AddNotification("Contact", "Contact not found");
        return new GenericCommandResult(false, "Contato não encontrado", command.Notifications);
      }

      contact.Update(command.Name!, command.Email!, command.Phone!);
      _repository.Contacts.Update(contact);

      return new GenericCommandResult(true, "Contato atualizado", contact);
    }

    public ICommandResult Handle(DeleteContactCommand command)
    {
      command.Validate();
      if (!command.IsValid)
      {
        return new GenericCommandResult(false, "Dados inválidos", command.Notifications);
      }

      var contact = _repository.Contacts.GetById(command.Id!, command.UserId!);

      if (contact == null)
      {
        command.AddNotification("Contact", "Contact not found");
        return new GenericCommandResult(false, "Contato não encontrado", command.Notifications);
      }

      _repository.Contacts.Delete(contact);

      return new GenericCommandResult(true, "Contato deletado com sucesso", null);
    }
  }

}