using AgendaNet.Domain.DTOs;
using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Services;

public interface IAuthService
{
  public bool ValidateCredentials(User user, string password);
  public TokenDTO GenerateToken(User user);
}