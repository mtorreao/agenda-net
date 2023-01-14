namespace AgendaNet.Domain.Entities
{
  public abstract class Entity : IEquatable<Entity>
  {
    protected Entity()
    {
      Id = Guid.NewGuid();
    }
    
    public Guid Id { get; private set; }

    public bool Equals(Entity? other)
    {
      return other != null && Id.Equals(other.Id);
    }
  }
}
