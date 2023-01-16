using AgendaNet.Domain.Commands;

namespace AgendaNet.Tests.Commands
{
  public class CreateContactCommandTest
  {
    private readonly CreateContactCommand _invalidCommand = new CreateContactCommand("", "", "", "");
    private readonly CreateContactCommand _validCommand = new CreateContactCommand("Test", "a@a.com", "81912341234", "1234");

    public CreateContactCommandTest()
    {
      _invalidCommand.Validate();
      _validCommand.Validate();
    }

    [Fact]
    public void Should_Be_Valid()
    {
      Assert.True(_validCommand.IsValid);
    }

    [Fact]
    public void Should_Be_Invalid()
    {
      Assert.False(_invalidCommand.IsValid);
    }

    [Fact]
    public void Should_Be_Invalid_If_Email_Is_Not_A_Valid_Email()
    {
      var command = new CreateContactCommand("Test", "a", "81912341234", null);
      command.Validate();
      Assert.NotNull(command.Notifications.FirstOrDefault(n => n.Key == "Email"));
      Assert.False(command.IsValid);
    }

  }
}


