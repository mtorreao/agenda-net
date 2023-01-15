using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Infra.Repositories
{
  public class IdentityRepository : IIdentityRepository
  {
    public IGenericRepository<User> Users { get; }

    public IdentityRepository(IGenericRepository<User> users)
    {
      Users = users;
    }
  }
}