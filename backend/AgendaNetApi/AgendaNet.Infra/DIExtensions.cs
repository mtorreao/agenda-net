using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AgendaNet.Domain.Commands;
using AgendaNet.Domain.DTOs;
using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using AgendaNet.Domain.Services;
using AgendaNet.Infra;
using AgendaNet.Infra.Initializers;
using AgendaNet.Infra.JWT;
using AgendaNet.Infra.Repositories;
using AgendaNet.Infra.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgendaNet;

[ExcludeFromCodeCoverage]
public static class DIExtensions
{
  public static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddScoped<IAuthService, AuthService>();

    return services;
  }

  public static IServiceCollection AddAutoMapper(this IServiceCollection services)
  {
    services.AddAutoMapper(Assembly.Load("AgendaNet.Infra"), Assembly.Load("AgendaNet.Domain"));
    // Configure AutoMapper
    var configuration = new MapperConfiguration(cfg =>
    {
      cfg.CreateMap<User, AuthUser>()
         .ConstructUsing(src => new AuthUser()
         {
           Email = src.Email,
           UserName = src.Email,
           PasswordHash = src.Password,
           Name = src.Name
         });

      cfg.CreateMap<AuthUser, User>()
         .ConstructUsing(src => new User(src.Name ?? "", src.Email, src.PasswordHash));

      cfg.CreateMap<SignUpCommand, User>()
         .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
         .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

      cfg.CreateMap<User, UserDTO>();

      cfg.CreateMap<Contact, ContactDTO>()
         .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")));
    });

    services.AddSingleton(configuration.CreateMapper());

    return services;
  }

  public static IServiceCollection AddAgendaNetDB(this IServiceCollection services, string connectionString)
  {
    var serverVersion = new MySqlServerVersion(new Version(5, 7));

    services.AddDbContext<DataContext>(
        dbContextOptions => dbContextOptions
            .UseMySql(connectionString, serverVersion, builder => builder
                .MigrationsAssembly("AgendaNet.Infra")
                .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
            )
            // The following three options help with debugging, but should
            // be changed or removed for production.
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());

    services.AddScoped<IRepository, Repository>();
    services.AddScoped<IGenericRepository<Contact>, ContactRepository>();
    services.AddScoped<InitializerDataDb>();

    return services;
  }

  public static IServiceCollection AddIdentityDb(this IServiceCollection services, string connectionString)
  {
    var serverVersion = new MySqlServerVersion(new Version(5, 7));

    services.AddDbContext<IdentityContext>(
        dbContextOptions => dbContextOptions.UseInMemoryDatabase("Identity"));

    services.AddScoped<InitializerIdentityDb>();
    services.AddScoped<IUserRepository<User>, UserRepository>();
    services.AddScoped<IIdentityRepository, IdentityRepository>();

    return services;
  }

  public static IServiceCollection AddJwtSecurity(
        this IServiceCollection services,
        TokenConfigurations tokenConfigurations)
  {
    // Ativando a utilização do ASP.NET Identity, a fim de
    // permitir a recuperação de seus objetos via injeção de
    // dependências
    services
      .AddIdentity<AuthUser, IdentityRole>(opt =>
        {
          opt.User.RequireUniqueEmail = true;
          opt.Password.RequiredLength = 8;
          opt.Password.RequireDigit = true;
          opt.Password.RequireNonAlphanumeric = false;
          opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
      .AddEntityFrameworkStores<IdentityContext>()
      .AddDefaultTokenProviders();

    var signingConfigurations = new SigningConfigurations(
        tokenConfigurations.SecretJwtKey!);
    services.AddSingleton(signingConfigurations);

    services.AddSingleton(tokenConfigurations);

    services.AddAuthentication(authOptions =>
    {
      authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(bearerOptions =>
    {
      var paramsValidation = bearerOptions.TokenValidationParameters;
      paramsValidation.IssuerSigningKey = signingConfigurations.Key;
      paramsValidation.ValidAudience = tokenConfigurations.Audience;
      paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

      // Valida a assinatura de um token recebido
      paramsValidation.ValidateIssuerSigningKey = true;

      // Verifica se um token recebido ainda é válido
      paramsValidation.ValidateLifetime = true;

      // Tempo de tolerância para a expiração de um token (utilizado
      // caso haja problemas de sincronismo de horário entre diferentes
      // computadores envolvidos no processo de comunicação)
      paramsValidation.ClockSkew = TimeSpan.Zero;
    });

    // Ativa o uso do token como forma de autorizar o acesso
    // a recursos deste projeto
    services.AddAuthorization(auth =>
    {
      auth.AddPolicy("Bearer",
        new AuthorizationPolicyBuilder()
              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
              .RequireAuthenticatedUser()
              .Build());
    });

    return services;
  }
}

