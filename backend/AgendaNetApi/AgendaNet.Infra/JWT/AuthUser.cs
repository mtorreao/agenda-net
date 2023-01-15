using Microsoft.AspNetCore.Identity;

namespace AgendaNet.Infra.JWT;

public class AuthUser : IdentityUser { 
  public string? Name { get; set; }
}