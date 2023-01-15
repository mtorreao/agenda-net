using AgendaNet.Domain.Commands;

namespace AgendaNet.Tests.Commands;

public class SignUpCommandTest
{
  [Fact]
  public void Password_Should_Be_GreaterOrEqualThan_8_Caracters()
  {
    var command = new SignUpCommand()
    {
      Email = "a@a.com",
      Password = "1234567",
      Name = "Teste",
    };
    command.Validate();
    Assert.False(command.IsValid);
    Assert.NotNull(command.Notifications.FirstOrDefault(x => x.Key == "Password"));
  }

  [Fact]
  public void Should_NotBe_A_Valid_Email()
  {
    var command = new SignUpCommand()
    {
      Email = "a.com",
      Password = "12345678",
      Name = "Teste",
    };
    command.Validate();
    Assert.False(command.IsValid);
    Assert.NotNull(command.Notifications.FirstOrDefault(x => x.Key == "Email"));
  }

  [Fact]
  public void Should_Name_Is_Required()
  {
    var command = new SignUpCommand()
    {
      Email = "a@a.com",
      Password = "12345678",
      Name = "",
    };
    command.Validate();
    Assert.False(command.IsValid);
    Assert.NotNull(command.Notifications.FirstOrDefault(x => x.Key == "Name"));
  }
}
