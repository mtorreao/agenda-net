using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeIdentityRepository : IIdentityRepository
{
  public IGenericRepository<User> Users => new FakeUserRepository();
}