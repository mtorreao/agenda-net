using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeIdentityRepository : IIdentityRepository
{
  private readonly FakeUserRepository _userRepository;
  public FakeIdentityRepository()
  {
    this._userRepository = new FakeUserRepository();
  }
  public IUserRepository<User> Users => _userRepository;
}