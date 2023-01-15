using AgendaNet;
using AgendaNet.Infra.Initializers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionsConfigurations = new ConnectionsConfigurations();
new ConfigureFromConfigurationOptions<ConnectionsConfigurations>(
    builder.Configuration.GetSection("ConnectionStrings"))
        .Configure(connectionsConfigurations);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHandlers();
builder.Services.AddServices();
builder.Services.AddAutoMapper();
builder.Services.AddAgendaNetDB(connectionsConfigurations.AgendaNetDB!);
builder.Services.AddIdentityDb(connectionsConfigurations.IdentityDB!);
builder.Services.AddJwtSecurity(tokenConfigurations);
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

  // Generate initial data for development databases
  using var scope = app.Services.CreateScope();
  scope.ServiceProvider.GetRequiredService<InitializerDataDb>().Init(); // Main MySQL database
  scope.ServiceProvider.GetRequiredService<InitializerIdentityDb>().Init(); // In memory database
}

app.UseHttpsRedirection();

app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();