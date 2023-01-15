using AgendaNet.Infra.JWT;
using Microsoft.AspNetCore.Identity;

namespace AgendaNet.Infra.Initializers;

public class InitializerIdentityDb
{
  private readonly IdentityContext _identityContext;
  private readonly UserManager<AuthUser> _userManager;
  private readonly RoleManager<IdentityRole> _roleManager;

  public InitializerIdentityDb(
      IdentityContext identityContext,
      UserManager<AuthUser> userManager,
      RoleManager<IdentityRole> roleManager)
  {
    _identityContext = identityContext;
    _userManager = userManager;
    _roleManager = roleManager;
  }

  public async void Init()
  {
    if (!(await _identityContext.Database.EnsureCreatedAsync()))
      throw new Exception("Erro durante a criação do banco de dados de identidade.");

    await CreateRolesAsync();

    await CreateUserAsync(
        new AuthUser()
        {
          UserName = "admin@test.com",
          Email = "admin@test.com",
          EmailConfirmed = true
        }, "Asdf1234", Roles.Admin);
  }

  private async Task CreateRolesAsync()
  {
    if (!(await _roleManager.RoleExistsAsync(Roles.Admin)))
    {
      var result = await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
      if (!result.Succeeded)
      {
        throw new Exception(
            $"Erro durante a criação da role {Roles.Admin}.");
      }
    }
  }

  private async Task CreateUserAsync(
      AuthUser user,
      string password,
      string? initialRole = null)
  {
    if (await _userManager.FindByEmailAsync(user.Email) == null)
    {
      var resultado = await _userManager.CreateAsync(user, password);

      // Create role for new user
      if (resultado.Succeeded && !String.IsNullOrWhiteSpace(initialRole))
      {
        await _userManager.AddToRoleAsync(user, initialRole);
      }
    }
  }
}
