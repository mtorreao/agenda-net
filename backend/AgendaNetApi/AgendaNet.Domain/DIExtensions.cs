using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AgendaNet;

[ExcludeFromCodeCoverage]
public static class DIExtensions
{
  public static IServiceCollection AddHandlers(this IServiceCollection services)
  {
    services.AddScoped<IHandler<CreateContactCommand>, ContactHandler>();
    services.AddScoped<IHandler<DeleteContactCommand>, ContactHandler>();
    services.AddScoped<IHandler<UpdateContactCommand>, ContactHandler>();
    services.AddScoped<IHandler<SignUpCommand>, AuthHandler>();
    services.AddScoped<IHandler<SignInCommand>, AuthHandler>();
    return services;
  }
}
