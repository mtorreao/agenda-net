using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AgendaNet.Infra.Repositories
{
  public class ContactRepository : IGenericRepository<Contact>
  {
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public ContactRepository(IDbContextFactory<DataContext> context)
    {
      _contextFactory = context;
    }

    public void Create(Contact entity)
    {
      using var context = _contextFactory.CreateDbContext();
      context.Contacts.Add(entity);
      context.SaveChanges();
    }

    public void Delete(Contact entity)
    {
      using var context = _contextFactory.CreateDbContext();
      context.Contacts.Remove(entity);
      context.SaveChanges();
    }

    public IEnumerable<Contact> GetAll(string? userId)
    {
      using var context = _contextFactory.CreateDbContext();
      if (userId == null)
        return context.Contacts.ToList();
      var guidUserId = Guid.Parse(userId);
      return context.Contacts.Where(c => c.UserId == guidUserId).ToList();
    }

    public Contact? GetById(string id, string? userId)
    {
      using var context = _contextFactory.CreateDbContext();
      var guidId = Guid.Parse(id);
      if (userId == null)
        return context.Contacts.FirstOrDefault(x => x.Id == guidId);
      return context.Contacts.FirstOrDefault(x => x.Id == guidId && x.UserId == Guid.Parse(userId));
    }

    public void Update(Contact entity)
    {
      using var context = _contextFactory.CreateDbContext();
      context.Contacts.Update(entity);
      context.SaveChanges();
    }
  }
}