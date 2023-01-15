using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;

namespace AgendaNet.Infra.Repositories
{
  public class ContactRepository : IGenericRepository<Contact>
  {
    private readonly DataContext _context;

    public ContactRepository(DataContext context)
    {
      _context = context;
    }

    public void Create(Contact entity)
    {
      _context.Contacts.Add(entity);
      _context.SaveChanges();
    }

    public void Delete(Contact entity)
    {
      _context.Contacts.Remove(entity);
      _context.SaveChanges();
    }

    public IEnumerable<Contact> GetAll(string? userId)
    {
      if (userId == null)
        return _context.Contacts.ToList();
      var guidUserId = Guid.Parse(userId);
      return _context.Contacts.Where(c => c.UserId == guidUserId).ToList();
    }

    public Contact? GetById(Guid id, string? userId)
    {
      if (userId == null)
        return _context.Contacts.FirstOrDefault(x => x.Id == id);
      return _context.Contacts.FirstOrDefault(x => x.Id == id && x.UserId == Guid.Parse(userId));
    }

    public void Update(Contact entity)
    {
      _context.Contacts.Update(entity);
      _context.SaveChanges();
    }
  }
}