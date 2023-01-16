using Microsoft.EntityFrameworkCore;

namespace AgendaNet.Infra.Initializers;

public class InitializerDataDb
{
  private readonly IDbContextFactory<DataContext> _dataContext;

  public InitializerDataDb(IDbContextFactory<DataContext> dataContext)
  {
    _dataContext = dataContext;
  }

  public void Init() {
    using var context = _dataContext.CreateDbContext();
    if (!context.Database.EnsureDeleted())
      throw new Exception("Erro durante a exclusão do banco de dados da aplicação.");
    if (!context.Database.EnsureCreated())
      throw new Exception("Erro durante a criação do banco de dados da aplicação.");
  }
}