using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeRepository : IRepository
{
  public IGenericRepository<Contact> Contacts { get; }
  public FakeRepository()
  {
    Contacts = new FakeContactRepository();
  }
}