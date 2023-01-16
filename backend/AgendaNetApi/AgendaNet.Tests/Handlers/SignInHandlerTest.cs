using AgendaNet.Domain.Commands;
using AgendaNet.Domain.DTOs;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Services;
using AgendaNet.Tests.Repositories;
using AutoMapper;
using Moq;

namespace AgendaNet.Tests.Handlers;

public class SignInHandlerTest
{

  private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
  private readonly Mock<IAuthService> _authService = new Mock<IAuthService>();

  [Fact]
  public void Should_Return_Error_On_Invalid_Input_Data()
  {
    var command = new SignInCommand("Test", "");
    var repository = new FakeIdentityRepository();
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.False(result.Success);
  }

  [Fact]
  public void Should_Return_An_Error_If_User_Not_Exists()
  {
    var command = new SignInCommand("a@a.com", "12345678");
    var repository = new FakeIdentityRepository();
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.False(result.Success);
  }

  [Fact]
  public void Should_Return_An_Error_If_Doesnt_Has_Valid_Credentials()
  {
    var command = new SignInCommand("a@a.com", "12345678");
    var repository = new FakeIdentityRepository();
    repository.Users.Create(new User("Test", "a@a.com", "12345678"));
    _authService.Setup(x => x.ValidateCredentials(It.IsAny<User>(), It.IsAny<string>())).Returns(false);
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.False(result.Success);
  }

  [Fact]
  public void Should_Return_Token_On_Login_Success()
  {
    var expectedToken = "1234567890";
    var command = new SignInCommand("a@a.com", "12345678");
    var repository = new FakeIdentityRepository();
    repository.Users.Create(new User("Test", "a@a.com", "12345678"));
    _authService.Setup(x => x.ValidateCredentials(It.IsAny<User>(), It.IsAny<string>())).Returns(true);
    _authService.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(new Domain.DTOs.TokenDTO() {
      AccessToken = expectedToken,
    });
    var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

    var result = (GenericCommandResult)handler.Handle(command);

    Assert.True(result.Success);
    Assert.Equal(((CreateUserDTO)result.Data!).Token!.AccessToken, expectedToken);
  }

  // [Fact]
  // public void Should_Create_A_User()
  // {
  //   var command = new SignInCommand("a@a.com", "12345678");
  //   var repository = new FakeIdentityRepository();
  //   _mapper.Setup(m => m.Map<User>(command)).Returns(new User("Test", "a@a.com", "12345678"));
  //   var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

  //   var result = (GenericCommandResult)handler.Handle(command);

  //   Assert.True(result.Success);
  //   Assert.NotNull(result.Data);
  // }

  // [Fact]
  // public void Should_Return_An_Error_If_Already_Has_An_User_With_This_Email()
  // {
  //   var command = new SignInCommand("Test", "a@a.com", "12345678");
  //   var repository = new FakeIdentityRepository();
  //   repository.Users.Create(new User("Test", "a@a.com", "12345678"));
  //   var handler = new AuthHandler(repository, _mapper.Object, _authService.Object);

  //   var result = (GenericCommandResult)handler.Handle(command);

  //   Assert.False(result.Success);
  //   Assert.NotNull(((IEnumerable<Notification>)result.Data!).FirstOrDefault(n => n.Key == "Email"));
  // }
}