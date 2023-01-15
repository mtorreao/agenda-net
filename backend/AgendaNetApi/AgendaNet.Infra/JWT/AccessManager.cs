using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using AgendaNet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AgendaNet.Infra.JWT;

public class AccessManager
{
  private UserManager<AuthUser> _userManager;
  private SignInManager<AuthUser> _signInManager;
  private SigningConfigurations _signingConfigurations;
  private TokenConfigurations _tokenConfigurations;

  public AccessManager(
      UserManager<AuthUser> userManager,
      SignInManager<AuthUser> signInManager,
      SigningConfigurations signingConfigurations,
      TokenConfigurations tokenConfigurations)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _signingConfigurations = signingConfigurations;
    _tokenConfigurations = tokenConfigurations;
  }

  public bool ValidateCredentials(User user)
  {
    bool isValid = false;
    if (user is not null && !String.IsNullOrWhiteSpace(user.Email.ToString()))
    {
      // Verifica a existência do usuário nas tabelas do
      // ASP.NET Identity
      var userIdentity = _userManager.FindByEmailAsync(user.Email.ToString()).Result;
      if (userIdentity is not null)
      {
        // Efetua o login com base no Id do usuário e sua senha
        var resultadoLogin = _signInManager
            .CheckPasswordSignInAsync(userIdentity, user.Password, false)
            .Result;
        if (resultadoLogin.Succeeded)
        {
          // Verifica se o usuário em questão possui
          // a role Acesso-APIs
          isValid = _userManager.IsInRoleAsync(
              userIdentity, Roles.Admin).Result;
        }
      }
    }

    return isValid;
  }

  public Token GenerateToken(User user)
  {
    ClaimsIdentity identity = new(
        new GenericIdentity(user.Id.ToString(), "Login"),
        new[] {
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
          new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString())
        }
    );

    DateTime dataCriacao = DateTime.Now;
    DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

    var handler = new JwtSecurityTokenHandler();
    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
    {
      Issuer = _tokenConfigurations.Issuer,
      Audience = _tokenConfigurations.Audience,
      SigningCredentials = _signingConfigurations.SigningCredentials,
      Subject = identity,
      NotBefore = dataCriacao,
      Expires = dataExpiracao
    });
    var token = handler.WriteToken(securityToken);

    return new()
    {
      Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
      Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
      AccessToken = token,
    };
  }
}