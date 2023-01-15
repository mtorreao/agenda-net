namespace AgendaNet.Infra.Initializers;

public class InitializerDataDb
{
  private readonly DataContext _dataContext;

  public InitializerDataDb(DataContext dataContext)
  {
    _dataContext = dataContext;
  }

  public void Init() {
    if (!_dataContext.Database.EnsureDeleted())
      throw new Exception("Erro durante a exclusão do banco de dados da aplicação.");
    if (!_dataContext.Database.EnsureCreated())
      throw new Exception("Erro durante a criação do banco de dados da aplicação.");
  }
}