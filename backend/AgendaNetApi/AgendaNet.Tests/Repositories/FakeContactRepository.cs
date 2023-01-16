using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Tests.Repositories;

public class FakeContactRepository : IGenericRepository<Contact>
{
  private List<Contact> _contacts = new List<Contact>();
  
  public void Create(Contact entity)
  {
    _contacts.Add(entity);
  }

  public void Delete(Contact entity)
  {
    _contacts.Remove(entity);
  }

  public IEnumerable<Contact> GetAll(string? userId)
  {
    return _contacts;
  }

  public Contact? GetById(string id, string? userId)
  {
    var guid = new Guid(id);
    return _contacts.FirstOrDefault(x => x.Id == guid);
  }

  public void Update(Contact entity)
  {
    var contact = _contacts.First(x => x.Id == entity.Id);
    var newContact = new Contact(entity.Name, entity.Email, entity.Phone, new Guid());
    _contacts.Remove(contact);
    _contacts.Add(newContact);
  }
}