using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Repositories;

public interface IIdentityRepository
{
  public IGenericRepository<User> Users { get; }
}