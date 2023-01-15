using AgendaNet.Domain.Handlers;
using AgendaNet.Domain.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaNet;

public static class DIExtensions
{
  public static IServiceCollection AddHandlers(this IServiceCollection services)
  {
    services.AddScoped<IHandler<CreateContactCommand>, ContactHandler>();
    services.AddScoped<IHandler<SignUpCommand>, AuthHandler>();
    return services;
  }
}
