using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Repositories
{
  public interface IRepository {
    public IGenericRepository<Contact> Contacts { get; }
  }
}