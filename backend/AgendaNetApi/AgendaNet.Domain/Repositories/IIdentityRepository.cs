using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Repositories;

public interface IIdentityRepository
{
  public IUserRepository<User> Users { get; }
}