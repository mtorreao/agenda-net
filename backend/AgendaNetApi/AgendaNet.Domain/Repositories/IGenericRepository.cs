using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Repositories
{
  public interface IGenericRepository<T> where T : Entity
  {
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? GetById(Guid id, string? userId);
    IEnumerable<T> GetAll(string? userId);
  }
}
