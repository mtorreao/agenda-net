using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using AgendaNet.Infra.JWT;
using AutoMapper;

namespace AgendaNet.Infra.Repositories
{
  public class UserRepository : IGenericRepository<User>
  {
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public UserRepository(IdentityContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public void Create(User entity)
    {
      _context.Users.Add(_mapper.Map<AuthUser>(entity));
      _context.SaveChanges();
    }

    public void Delete(User entity)
    {
      _context.Users.Remove(_mapper.Map<AuthUser>(entity));
      _context.SaveChanges();
    }


    public IEnumerable<User> GetAll()
    {
      return _mapper.Map<IEnumerable<User>>(_context.Users.ToList());
    }

    public User GetById(Guid id)
    {
      return _mapper.Map<User>(_context.Users.FirstOrDefault(x => x.Id == id.ToString()));
    }

    public void Update(User entity)
    {
      _context.Users.Update(_mapper.Map<AuthUser>(entity));
      _context.SaveChanges();
    }
  }
}