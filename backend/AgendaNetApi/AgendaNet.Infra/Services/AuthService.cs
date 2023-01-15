using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using AgendaNet.Domain.DTOs;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Services;
using AgendaNet.Infra.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AgendaNet.Infra.Services;

public class AuthService : IAuthService
{
  private UserManager<AuthUser> _userManager;
  private SignInManager<AuthUser> _signInManager;
  private SigningConfigurations _signingConfigurations;
  private TokenConfigurations _tokenConfigurations;

  public AuthService(
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

  public TokenDTO GenerateToken(User user)
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

    return new TokenDTO()
    {
      Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
      AccessToken = token,
    };
  }

  public bool ValidateCredentials(User user, string password)
  {
    bool isValid = false;
    if (user is not null && !String.IsNullOrWhiteSpace(user.Email.ToString()))
    {
      // Verifica a existência do usuário nas tabelas do Identity
      var userIdentity = _userManager.FindByEmailAsync(user.Email.ToString()).Result;
      if (userIdentity is not null)
      {
        // Efetua o login com base no email do usuário e sua senha
        var resultSignIn = _signInManager
            .CheckPasswordSignInAsync(userIdentity, password, false)
            .Result;
        isValid = resultSignIn.Succeeded;
      }
    }

    return isValid;
  }
}