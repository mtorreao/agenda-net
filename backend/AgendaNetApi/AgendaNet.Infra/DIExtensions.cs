using AgendaNet.Domain.Entities;
using AgendaNet.Domain.Repositories;
using AgendaNet.Infra;
using AgendaNet.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgendaNet;

public static class DIExtensions
{
  public static IServiceCollection AddAgendaNetDB(this IServiceCollection services)
  {
    var connectionString = "Server=localhost;User=agenda_net;Password=agenda_net;SslMode=None;Database=agenda_net_db";

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
    return services;
  }
}

