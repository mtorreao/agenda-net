using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Handlers;
using AgendaNet.Tests.Repositories;

namespace AgendaNet.Tests.Handlers
{
  public class DeleteContactHandlerTest
  {

    [Fact]
    public void Should_Return_An_Error_On_Invalid_Inputs()
    {
      var command = new DeleteContactCommand("", "");
      var handler = new ContactHandler(new FakeRepository());
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.False(result.Success);
    }

    [Fact]
    public void Should_Return_An_Error_If_Contact_Is_Not_Found()
    {
      var command = new DeleteContactCommand(Guid.NewGuid().ToString(), new Guid().ToString());
      var handler = new ContactHandler(new FakeRepository());
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.False(result.Success);
    }

    [Fact]
    public void Should_Delete_Contact()
    {
      var command = new DeleteContactCommand(Guid.NewGuid().ToString(), new Guid().ToString());
      FakeRepository repository = new FakeRepository();
      Contact entity = new Contact("Test", "a@a.com", "123", new Guid(command.UserId!));
      repository.Contacts.Create(entity);
      var handler = new ContactHandler(repository);
      command.Id = entity.Id.ToString();
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.True(result.Success);
    }
  }

}