using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Messages;
using AgendaNet.Tests.Repositories;
using Moq;

namespace AgendaNet.Tests.Handlers
{
  public class UpdateContactHandlerTest
  {

    [Fact]
    public void Should_Return_An_Error_On_Invalid_Inputs()
    {
      var command = new UpdateContactCommand(Guid.NewGuid().ToString(), "", "", "", new Guid().ToString());
      var handler = new ContactHandler(new FakeRepository(), new Mock<IMessageProducer>().Object);
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.False(result.Success);
    }

    [Fact]
    public void Should_Return_An_Error_If_Contact_Is_Not_Found()
    {
      var command = new UpdateContactCommand(Guid.NewGuid().ToString(), "Teste", "a@a.com", "12345678", new Guid().ToString());
      var handler = new ContactHandler(new FakeRepository(), new Mock<IMessageProducer>().Object);
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.False(result.Success);
    }

    [Fact]
    public void Should_Update_Contact()
    {
      var command = new UpdateContactCommand(Guid.NewGuid().ToString(), "Teste", "a@a.com", "12345678", new Guid().ToString());
      FakeRepository repository = new FakeRepository();
      Contact entity = new Contact(command.Name!, command.Email!, command.Phone!, new Guid(command.UserId!));
      repository.Contacts.Create(entity);
      var handler = new ContactHandler(repository, new Mock<IMessageProducer>().Object);
      command.Id = entity.Id.ToString();
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.True(result.Success);
    }
  }

}