using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeUserRepository : IGenericRepository<User>
{
  private List<User> _users = new List<User>();

  public void Create(User entity)
  {
    _users.Add(entity);
  }

  public void Delete(User entity)
  {
    _users.Remove(entity);
  }

  public IEnumerable<User> GetAll()
  {
    return _users;
  }

  public User GetById(Guid id)
  {
    return _users.First(x => x.Id == id);
  }

  public void Update(User entity)
  {
    var user = _users.First(x => x.Id == entity.Id);
    var newUser = new User(entity.Name, entity.Email, entity.Password);
    _users.Remove(user);
    _users.Add(newUser);
  }
}