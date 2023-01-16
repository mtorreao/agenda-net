using AgendaNet.Domain.Commands;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Services;
using AgendaNet.Tests.Repositories;
using AutoMapper;
using Flunt.Notifications;
using Moq;

namespace AgendaNet.Tests.Handlers;

public class SignUpHandlerTest
{
  private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
  private readonly Mock<IAuthService> _authService = new Mock<IAuthService>();

  [Fact]
  public void Should_Return_Error_On_Invalid_Input_Data()
  {
    var command = new SignUpCommand("Test", "a@a.com", "");
    var repository = new FakeIdentityRepository();
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.False(result.Success);
  }

  [Fact]
  public void Should_Create_A_User()
  {
    var command = new SignUpCommand("Test", "a@a.com", "12345678");
    var repository = new FakeIdentityRepository();
    _mapper.Setup(m => m.Map<User>(command)).Returns(new User("Test", "a@a.com", "12345678"));
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.True(result.Success);
    Assert.NotNull(result.Data);
  }

  [Fact]
  public void Should_Return_An_Error_If_Already_Has_An_User_With_This_Email()
  {
    var command = new SignUpCommand("Test", "a@a.com", "12345678");
    var repository = new FakeIdentityRepository();
    repository.Users.Create(new User("Test", "a@a.com", "12345678"));
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.False(result.Success);
    Assert.NotNull(((IEnumerable<Notification>)result.Data!).FirstOrDefault(n => n.Key == "Email"));
  }
}