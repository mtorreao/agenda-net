using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Repositories
{
  public interface IUserRepository<T> where T : User
  {
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T GetById(Guid id);
    T GetByEmail(string email);
  }
}
