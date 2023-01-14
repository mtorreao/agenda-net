using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeRepository : IRepository
{
  public IGenericRepository<Contact> Contacts => new FakeContactRepository();
}