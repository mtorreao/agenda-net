using AgendaNet.Domain.Entities;

namespace AgendaNet.Domain.Services;

public interface IHasher
{
  string HashPassword(User user, string password);
  bool VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
}