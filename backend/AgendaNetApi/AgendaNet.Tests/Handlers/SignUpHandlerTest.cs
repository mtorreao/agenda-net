using AgendaNet.Domain.Commands;

namespace AgendaNet.Tests.Handlers;

public class SignUpHandlerTest
{
  [Fact]
  public void Should_Create_A_User()
  {
    var command = new SignUpCommand("Test", "a@a.com", "12345678");
    // var handler = new AuthHandler(new FakeIdentityRepository());

    // var result = (GenericCommandResult)handler.Handle(command);

    // Assert.True(result.Success);
    // Assert.NotNull(result.Data);
  }
}