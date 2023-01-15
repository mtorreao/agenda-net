using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AgendaNet.Infra.JWT;

public class Hasher : IHasher
{
  private readonly IMapper _mapper;

  public Hasher(IMapper mapper)
  {
    _mapper = mapper;
  }

  public string HashPassword(User user, string password)
  {
    var hasher = new PasswordHasher<AuthUser>();
    var hashedPassword = hasher.HashPassword(_mapper.Map<AuthUser>(user), password);
    return hashedPassword;
  }

  public bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
  {
    return new PasswordHasher<AuthUser>().VerifyHashedPassword(_mapper.Map<AuthUser>(user), hashedPassword, providedPassword) != PasswordVerificationResult.Failed;
  }
}