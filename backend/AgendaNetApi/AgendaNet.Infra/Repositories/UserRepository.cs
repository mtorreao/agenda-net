using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using AgendaNet.Infra.JWT;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AgendaNet.Infra.Repositories
{
  public class UserRepository : IUserRepository<User>
  {
    private readonly UserManager<AuthUser> _context;
    private readonly IMapper _mapper;

    public UserRepository(UserManager<AuthUser> context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public void Create(User entity)
    {
      _context.CreateAsync(_mapper.Map<AuthUser>(entity), entity.Password);
    }

    public void Delete(User entity)
    {
      _context.DeleteAsync(_mapper.Map<AuthUser>(entity));
    }

    public User? GetByEmail(string email)
    {
      return _mapper.Map<AuthUser, User>(_context.FindByEmailAsync(email).Result);
    }

    public User? GetById(string id)
    {
      return _mapper.Map<User>(_context.FindByIdAsync(id).Result);
    }

    public void Update(User entity)
    {
      _context.UpdateAsync(_mapper.Map<AuthUser>(entity));
    }
  }
}