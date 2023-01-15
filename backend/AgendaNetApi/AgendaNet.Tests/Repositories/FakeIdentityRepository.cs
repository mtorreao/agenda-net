using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeIdentityRepository : IIdentityRepository
{
  public IUserRepository<User> Users => new FakeUserRepository();
}