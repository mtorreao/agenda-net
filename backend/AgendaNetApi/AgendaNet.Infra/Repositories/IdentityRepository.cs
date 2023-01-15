using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Infra.Repositories
{
  public class IdentityRepository : IIdentityRepository
  {
    public IUserRepository<User> Users { get; }

    public IdentityRepository(IUserRepository<User> users)
    {
      Users = users;
    }
  }
}