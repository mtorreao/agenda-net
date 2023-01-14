using AgendaNet.Domain.Commands;

namespace AgendaNet.Tests.Commands
{
  [TestClass]
  public class CreateContactCommandTest
  {
    private readonly CreateContactCommand _invalidCommand = new CreateContactCommand("", "", "");
    private readonly CreateContactCommand _validCommand = new CreateContactCommand("Test", "a@a.com", "81912341234");

    public CreateContactCommandTest()
    {
      _invalidCommand.Validate();
      _validCommand.Validate();
    }

    [TestMethod]
    public void Should_Be_Valid()
    {
      Assert.AreEqual(_validCommand.IsValid, true);
    }

    [TestMethod]
    public void Should_Be_Invalid()
    {
      Assert.AreEqual(_invalidCommand.IsValid, false);
    }

    [TestMethod]
    public void Should_Be_Invalid_If_Email_Is_Not_A_Valid_Email()
    {
      var command = new CreateContactCommand("Test", "a", "81912341234");
      command.Validate();
      Assert.IsNotNull(command.Notifications.FirstOrDefault(n => n.Key == "Email"));
      Assert.AreEqual(command.IsValid, false);
    }

  }
}


