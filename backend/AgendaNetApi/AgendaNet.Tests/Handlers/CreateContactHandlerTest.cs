using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Handlers;
using AgendaNet.Tests.Repositories;

namespace AgendaNet.Tests.Handlers
{
  public class CreateContactHandlerTest
  {

    [Fact]
    public void Should_Create_A_Contact()
    {
      var command = new CreateContactCommand("Test", "a@a.com", "81912341234", new Guid().ToString());
      var handler = new ContactHandler(new FakeRepository());
      var result = (GenericCommandResult)handler.Handle(command);
      Assert.True(result.Success);
    }
  }

}